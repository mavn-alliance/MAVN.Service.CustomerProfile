using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Log;
using MAVN.Common;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Publisher;
using MAVN.Persistence.PostgreSQL.Legacy;
using MAVN.Service.CustomerProfile.Contract;
using MAVN.Service.CustomerProfile.Domain.Enums;
using MAVN.Service.CustomerProfile.Domain.Models;
using MAVN.Service.CustomerProfile.Domain.Repositories;
using MAVN.Service.CustomerProfile.Domain.Services;
using MAVN.Service.Dictionaries.Client;

namespace MAVN.Service.CustomerProfile.DomainServices
{
    public class CustomerProfileService : ICustomerProfileService
    {
        private readonly ICustomerProfileRepository _customerProfileRepository;
        private readonly IDictionariesClient _dictionariesClient;
        private readonly IRabbitPublisher<EmailVerifiedEvent> _emailVerifiedPublisher;
        private readonly IRabbitPublisher<CustomerPhoneVerifiedEvent> _phoneVerifiedPublisher;
        private readonly IRabbitPublisher<CustomerProfileDeactivationRequestedEvent> _deactivationRequestedPublisher;
        private readonly ITransactionRunner _transactionRunner;
        private readonly ILog _log;

        public CustomerProfileService(
            ICustomerProfileRepository customerProfileRepository,
            IDictionariesClient dictionariesClient,
            IRabbitPublisher<EmailVerifiedEvent> emailVerifiedPublisher,
            IRabbitPublisher<CustomerPhoneVerifiedEvent> phoneVerifiedPublisher,
            IRabbitPublisher<CustomerProfileDeactivationRequestedEvent> deactivationRequestedPublisher,
            ITransactionRunner transactionRunner,
            ILogFactory logFactory)
        {
            _customerProfileRepository = customerProfileRepository;
            _dictionariesClient = dictionariesClient;
            _emailVerifiedPublisher = emailVerifiedPublisher;
            _phoneVerifiedPublisher = phoneVerifiedPublisher;
            _deactivationRequestedPublisher = deactivationRequestedPublisher;
            _transactionRunner = transactionRunner;
            _log = logFactory.CreateLog(this);
        }

        public async Task<CustomerProfileResult> GetByCustomerIdAsync(string customerId, bool includeNotVerified = false, bool includeNotActive = false)
        {
            var customer = await _customerProfileRepository.GetByCustomerIdAsync(customerId, includeNotVerified, includeNotActive);

            if (customer == null)
            {
                _log.Warning("Customer profile not found", context: customerId);

                return new CustomerProfileResult { ErrorCode = CustomerProfileErrorCodes.CustomerProfileDoesNotExist };
            }

            return new CustomerProfileResult { Profile = customer };
        }

        public async Task<PaginatedCustomerProfilesModel> GetPaginatedAsync(int currentPage, int pageSize, bool includeNotVerified = false, bool includeNotActive = false)
        {
            if (currentPage < 1)
                throw new ArgumentException("Current page can't be negative", nameof(currentPage));

            if (pageSize == 0)
                throw new ArgumentException("Page size can't be 0", nameof(pageSize));

            var skip = (currentPage - 1) * pageSize;
            var take = pageSize;

            var profiles = await _customerProfileRepository.GetPaginatedAsync(skip, take, includeNotVerified, includeNotActive);

            var totalCount = await _customerProfileRepository.GetTotalAsync(includeNotVerified, includeNotActive);

            return new PaginatedCustomerProfilesModel
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                Customers = profiles,
                TotalCount = totalCount
            };
        }

        public async Task<CustomerProfileErrorCodes> CreateIfNotExistsAsync(ICustomerProfile customerProfile)
        {
            var isSocialAccountProfile =
                customerProfile.LoginProviders.Any(p => p != LoginProvider.Standard);

            if (isSocialAccountProfile)
                customerProfile.IsEmailVerified = true;

            if (customerProfile.CountryOfNationalityId.HasValue)
            {
                var countryResult =
                    await _dictionariesClient.Salesforce.GetCountryOfResidenceByIdAsync(
                        customerProfile.CountryOfNationalityId.Value);

                if (countryResult == null)
                    return CustomerProfileErrorCodes.InvalidCountryOfNationalityId;
            }

            var creationResult = await _customerProfileRepository.CreateIfNotExistAsync(customerProfile);

            switch (creationResult)
            {
                case CustomerProfileErrorCodes.CustomerProfileAlreadyExistsWithDifferentProvider:
                    _log.Warning("Customer Profile already exists but with different login provider",
                        context: customerProfile.CustomerId);
                    return creationResult;
                case CustomerProfileErrorCodes.CustomerProfileAlreadyExists:
                    _log.Warning("Customer Profile already exists", context: customerProfile.CustomerId);
                    return creationResult;
            }

            _log.Info("Customer profile is created", context: customerProfile.CustomerId);

            if (isSocialAccountProfile)
            {
                Task.Run(async () =>
                {
                    try
                    {
                        await _emailVerifiedPublisher.PublishAsync(new EmailVerifiedEvent
                        {
                            CustomerId = customerProfile.CustomerId,
                            TimeStamp = DateTime.UtcNow
                        });
                    }
                    catch (Exception e)
                    {
                        _log.Error(e);
                    }
                });
            }

            return creationResult;
        }


        public async Task RemoveAsync(string customerId)
        {
            var deleted = await _customerProfileRepository.DeleteAsync(customerId);

            if (deleted)
                _log.Info("Customer profile was removed", context: customerId);
            else
                _log.Warning("Customer profile was not removed", context: customerId);
        }

        /// <inheritdoc />
        public async Task<CustomerProfileResult> GetByEmailAsync(string email, bool includeNotVerified = false, bool includeNotActive = false)
        {
            var customer = await _customerProfileRepository.GetByCustomerEmailAsync(email, includeNotVerified, includeNotActive);

            if (customer == null)
            {
                _log.Info("Customer profile not found", context: email.SanitizeEmail());

                return new CustomerProfileResult { ErrorCode = CustomerProfileErrorCodes.CustomerProfileDoesNotExist };
            }

            return new CustomerProfileResult { Profile = customer };
        }

        public async Task<CustomerProfileResult> GetByPhoneAsync(string phone, bool includeNotVerified = false, bool includeNotActive = false)
        {
            var customer = await _customerProfileRepository.GetByCustomerPhoneAsync(phone, includeNotVerified, includeNotActive);

            if (customer == null)
            {
                _log.Info("Customer profile not found", context: phone.SanitizePhone());

                return new CustomerProfileResult { ErrorCode = CustomerProfileErrorCodes.CustomerProfileDoesNotExist };
            }

            return new CustomerProfileResult { Profile = customer };
        }

        public async Task<CustomerProfileErrorCodes> SetEmailAsVerifiedAsync(string customerId)
        {
            var (error, wasEmailEverVerified) = await _customerProfileRepository.SetEmailVerifiedAsync(customerId);

            switch (error)
            {
                case CustomerProfileErrorCodes.CustomerProfileDoesNotExist:
                    _log.Warning("Customer Profile does not exists", context: customerId);
                    break;
                case CustomerProfileErrorCodes.CustomerProfileEmailAlreadyVerified:
                    _log.Warning("Customer profile email is already verified", context: customerId);
                    break;
                default:
                    _log.Info("Customer profile email is successfully verified", context: customerId);
                    await _emailVerifiedPublisher.PublishAsync(
                        new EmailVerifiedEvent
                        {
                            CustomerId = customerId,
                            TimeStamp = DateTime.UtcNow,
                            WasEmailEverVerified = wasEmailEverVerified
                        });
                    break;
            }

            return error;
        }

        public async Task<CustomerProfileErrorCodes> SetPhoneAsVerifiedAsync(string customerId)
        {
            var customerProfile =
                await _customerProfileRepository.GetByCustomerIdAsync(customerId, includeNotVerified: true);

            if (customerProfile == null)
                return CustomerProfileErrorCodes.CustomerProfileDoesNotExist;

            if (string.IsNullOrEmpty(customerProfile.PhoneNumber))
                return CustomerProfileErrorCodes.CustomerProfilePhoneIsMissing;

            var isPhoneNumberUsedByAnotherCustomer =
                await _customerProfileRepository.IsPhoneNumberUsedByAnotherCustomer(customerId, customerProfile.PhoneNumber);

            if (isPhoneNumberUsedByAnotherCustomer)
                return CustomerProfileErrorCodes.PhoneAlreadyExists;

            var (error, wasPhoneEverVerified) = await _customerProfileRepository.SetPhoneAsVerifiedAsync(customerId);
            switch (error)
            {
                case CustomerProfileErrorCodes.CustomerProfileDoesNotExist:
                    _log.Warning("Customer Profile does not exists", context: customerId);
                    break;
                case CustomerProfileErrorCodes.CustomerProfilePhoneAlreadyVerified:
                    _log.Warning("Customer profile phone is already verified", context: customerId);
                    break;
                case CustomerProfileErrorCodes.None:
                    _log.Info("Customer profile phone is successfully verified", context: customerId);
                    await _phoneVerifiedPublisher.PublishAsync(new CustomerPhoneVerifiedEvent
                    {
                        CustomerId = customerId,
                        WasPhoneEverVerified = wasPhoneEverVerified,
                        Timestamp = DateTime.UtcNow
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return error;
        }

        public Task<IEnumerable<ICustomerProfile>> GetByCustomerIdsAsync(IEnumerable<string> ids, bool includeNotVerified = false, bool includeNotActive = false)
        {
            return _customerProfileRepository.GetByIdsAsync(ids, includeNotVerified, includeNotActive);
        }

        public async Task<CustomerProfileErrorCodes> UpdateAsync(string customerId, string firstName, string lastName,
            string phoneNumber, int countryPhoneCodeId, int countryOfResidenceId)
        {
            var countryPhoneCode = await _dictionariesClient.Salesforce.GetCountryPhoneCodeByIdAsync(countryPhoneCodeId);

            if (countryPhoneCode == null)
                return CustomerProfileErrorCodes.InvalidCountryPhoneCodeId;

            var e164FormattedNumber = PhoneUtils.GetE164FormattedNumber(phoneNumber, countryPhoneCode.IsoCode);

            if (e164FormattedNumber == null)
                return CustomerProfileErrorCodes.InvalidPhoneNumber;

            var isPhoneNumberUsedByAnotherCustomer =
                await _customerProfileRepository.IsPhoneNumberUsedByAnotherCustomer(customerId, e164FormattedNumber);

            if (isPhoneNumberUsedByAnotherCustomer)
                return CustomerProfileErrorCodes.PhoneAlreadyExists;

            var result = await _customerProfileRepository.UpdateAsync(customerId, firstName, lastName, e164FormattedNumber,
                phoneNumber, countryPhoneCodeId, countryOfResidenceId);

            if (result == CustomerProfileErrorCodes.CustomerProfileDoesNotExist)
                _log.Warning("Customer profile does not exist so it was not updated", context: customerId);

            return result;
        }

        public async Task<CustomerProfileErrorCodes> UpdateEmailAsync(string customerId, string email)
        {
            var result = await _customerProfileRepository.UpdateEmailAsync(customerId, email, false);

            if (result == CustomerProfileErrorCodes.CustomerProfileDoesNotExist)
                _log.Warning("Customer id was not found, so customer email is not updated", context: customerId);

            return result;
        }

        public async Task<CustomerProfileErrorCodes> UpdateTierAsync(string customerId, string tierId)
        {
            var result = await _customerProfileRepository.UpdateTierAsync(customerId, tierId);

            if (result != CustomerProfileErrorCodes.None)
                _log.Warning("Customer reward tier not updated", context: $"customerId: {customerId}; error: {result}");

            return result;
        }

        public async Task<CustomerProfileErrorCodes> UpdatePhoneInfoAsync(string customerId, string phoneNumber, int countryPhoneCodeId)
        {
            var customerProfile =
                await _customerProfileRepository.GetByCustomerIdAsync(customerId, includeNotVerified: true);

            if (customerProfile == null)
                return CustomerProfileErrorCodes.CustomerProfileDoesNotExist;

            var countryPhoneCode = await _dictionariesClient.Salesforce.GetCountryPhoneCodeByIdAsync(countryPhoneCodeId);

            if (countryPhoneCode == null)
                return CustomerProfileErrorCodes.InvalidCountryPhoneCodeId;

            var e164FormattedNumber = PhoneUtils.GetE164FormattedNumber(phoneNumber, countryPhoneCode.IsoCode);

            if(e164FormattedNumber == null)
                return CustomerProfileErrorCodes.InvalidPhoneNumber;

            var isPhoneNumberUsedByAnotherCustomer =
                await _customerProfileRepository.IsPhoneNumberUsedByAnotherCustomer(customerId, e164FormattedNumber);

            if (isPhoneNumberUsedByAnotherCustomer)
                return CustomerProfileErrorCodes.PhoneAlreadyExists;

            if (customerProfile.PhoneNumber == e164FormattedNumber &&
                customerProfile.ShortPhoneNumber == phoneNumber &&
                customerProfile.CountryPhoneCodeId == countryPhoneCodeId)
            {
                return CustomerProfileErrorCodes.None;
            }

            var result = await _customerProfileRepository.UpdatePhoneInfoAsync(customerId, e164FormattedNumber,
                phoneNumber, countryPhoneCodeId, isPhoneVerified: false);

            return result;
        }

        public async Task<CustomerProfileErrorCodes> RequestCustomerProfileDeactivation(string customerId)
        {
            return await _transactionRunner.RunWithTransactionAsync(async txContext =>
            {
                var customerProfile =
                    await _customerProfileRepository.GetByCustomerIdAsync(customerId, includeNotVerified: true, includeNotActive: true, txContext);

                if (customerProfile == null)
                    return CustomerProfileErrorCodes.CustomerProfileDoesNotExist;

                if (customerProfile.Status != CustomerProfileStatus.Active)
                    return CustomerProfileErrorCodes.CustomerIsNotActive;

                await _customerProfileRepository.ChangeProfileStatus(customerId, CustomerProfileStatus.PendingDeactivation, txContext);

                await _deactivationRequestedPublisher.PublishAsync(new CustomerProfileDeactivationRequestedEvent
                {
                    CustomerId = customerId
                });

                return CustomerProfileErrorCodes.None;
            });
        }

        public async Task MarkCustomerAsDeactivated(string customerId)
        {
            await _transactionRunner.RunWithTransactionAsync(async txContext =>
            {
                var customerProfile =
                    await _customerProfileRepository.GetByCustomerIdAsync(customerId, includeNotVerified: true, includeNotActive: true, txContext);

                if (customerProfile == null)
                {
                    _log.Warning("Could not find customer profile for a customer which deactivation has already been completed");
                    return;
                }

                if (customerProfile.Status != CustomerProfileStatus.PendingDeactivation)
                {
                    _log.Warning("Customer profile is not in the correct status to be marked as deactivated",
                        context: new { customerId, customerProfile.Status });
                    return;
                }

                await _customerProfileRepository.ChangeProfileStatus(customerId, CustomerProfileStatus.Deactivated, txContext);
            });
        }
    }
}
