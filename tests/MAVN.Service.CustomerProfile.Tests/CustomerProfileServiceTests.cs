using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lykke.Common.MsSql;
using Lykke.Logs;
using Lykke.Logs.Loggers.LykkeConsole;
using Lykke.RabbitMqBroker.Publisher;
using MAVN.Service.CustomerProfile.Contract;
using MAVN.Service.CustomerProfile.Domain.Enums;
using MAVN.Service.CustomerProfile.Domain.Models;
using MAVN.Service.CustomerProfile.Domain.Repositories;
using MAVN.Service.CustomerProfile.DomainServices;
using MAVN.Service.CustomerProfile.MsSqlRepositories;
using Lykke.Service.Dictionaries.Client;
using Lykke.Service.Dictionaries.Client.Models.Salesforce;
using Moq;
using Xunit;

namespace MAVN.Service.CustomerProfile.Tests
{
    public class CustomerProfileServiceTests
    {
        private readonly Mock<IDictionariesClient> _dictionariesClientMock = new Mock<IDictionariesClient>();
        private readonly Mock<ICustomerProfileRepository> _customerProfileRepository = new Mock<ICustomerProfileRepository>();
        private readonly Mock<IRabbitPublisher<CustomerPhoneVerifiedEvent>> _phoneVerifiedPublisher = new Mock<IRabbitPublisher<CustomerPhoneVerifiedEvent>>();
        private readonly Mock<IRabbitPublisher<EmailVerifiedEvent>> _emailVerifiedPublisher = new Mock<IRabbitPublisher<EmailVerifiedEvent>>();
        private readonly Mock<IRabbitPublisher<CustomerProfileDeactivationRequestedEvent>> _deactivationRequestPublisher =
            new Mock<IRabbitPublisher<CustomerProfileDeactivationRequestedEvent>>();

        private readonly List<CountryPhoneCodeModel> _countryPhoneCodes = new List<CountryPhoneCodeModel>
        {
            new CountryPhoneCodeModel {Id = 1, CountryName = "Unknown", Code = "001", IsoCode = "+1"}
        };
        
        public CustomerProfileServiceTests()
        {
            _dictionariesClientMock.Setup(o => o.Salesforce.GetCountryPhoneCodesAsync())
                .Returns(() => Task.FromResult<IReadOnlyList<CountryPhoneCodeModel>>(_countryPhoneCodes));
        }
        
        [Fact]
        public async Task TryingToCreateStandardCustomerProfile_EverythingValid_SuccessfullyCreated()
        {
            _emailVerifiedPublisher.Setup(x => x.PublishAsync(It.IsAny<EmailVerifiedEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var customerProfileResult = CustomerProfileErrorCodes.None;
            _customerProfileRepository
                .Setup(x => x.CreateIfNotExistAsync(It.IsAny<CustomerProfileModel>()))
                .ReturnsAsync(customerProfileResult)
                .Verifiable();

            _dictionariesClientMock.Setup(x => x.Salesforce.GetCountryOfResidenceByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new CountryOfResidenceModel());

            var customerProfileService = CreateSutInstance();

            var actual = await customerProfileService.CreateIfNotExistsAsync(new CustomerProfileModel
            {
                LoginProviders = new List<LoginProvider>
                {
                    LoginProvider.Standard
                }
            });

            _customerProfileRepository.Verify();
            _emailVerifiedPublisher.Verify(x => x.PublishAsync(It.IsAny<EmailVerifiedEvent>()), Times.Never);

            Assert.Equal(customerProfileResult, actual);
        }

        [Fact]
        public async Task TryingToCreateSocialCustomerProfile_EverythingValid_SuccessfullyCreated()
        {
            _emailVerifiedPublisher.Setup(x => x.PublishAsync(It.IsAny<EmailVerifiedEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var customerProfileResult = CustomerProfileErrorCodes.None;
            _customerProfileRepository
                .Setup(x => x.CreateIfNotExistAsync(It.IsAny<CustomerProfileModel>()))
                .ReturnsAsync(customerProfileResult)
                .Verifiable();

            _dictionariesClientMock.Setup(x => x.Salesforce.GetCountryOfResidenceByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new CountryOfResidenceModel());

            var customerProfileService = CreateSutInstance();

            var actual = await customerProfileService.CreateIfNotExistsAsync(new CustomerProfileModel
            {
                CustomerId = "custId",
                LoginProviders = new List<LoginProvider>
                {
                    LoginProvider.Google
                }
            });

            _customerProfileRepository.Verify();

            Assert.Equal(customerProfileResult, actual);
        }

        [Fact]
        public async Task TryingToCreateCustomerProfile_InvalidCountryOfNationalityId_ErrorCodeIsReturned()
        {
            _dictionariesClientMock.Setup(x => x.Salesforce.GetCountryOfResidenceByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((CountryOfResidenceModel) null);


            var customerProfileService = CreateSutInstance();

            var actual = await customerProfileService.CreateIfNotExistsAsync(new CustomerProfileModel
            {
                LoginProviders = new List<LoginProvider>(),
                CountryOfNationalityId = 1
            });

            Assert.Equal(CustomerProfileErrorCodes.InvalidCountryOfNationalityId, actual);
        }

        [Fact]
        public async Task TryingToCreateCustomerProfile_CustomerAlreadyExists_ErrorCodeIsReturned()
        {
            var customerProfileResult = CustomerProfileErrorCodes.CustomerProfileAlreadyExists;

            _dictionariesClientMock.Setup(x => x.Salesforce.GetCountryOfResidenceByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new CountryOfResidenceModel());

            _customerProfileRepository
                .Setup(x => x.CreateIfNotExistAsync(It.IsAny<CustomerProfileModel>()))
                .ReturnsAsync(customerProfileResult);

            var customerProfileService = CreateSutInstance();

            var actual = await customerProfileService.CreateIfNotExistsAsync(new CustomerProfileModel
            {
                LoginProviders = new List<LoginProvider>()
            });

            Assert.Equal(customerProfileResult, actual);
        }

        [Fact]
        public async Task TryingToGetCustomerProfileByCustomerId_ValidCustomerId_SuccessfullyReturned()
        {
            var customerProfile = new CustomerProfileModel();
            _customerProfileRepository
                .Setup(x => x.GetByCustomerIdAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<TransactionContext>()))
                .ReturnsAsync(customerProfile)
                .Verifiable();

            var customerProfileService = CreateSutInstance();

            var actual = await customerProfileService.GetByCustomerIdAsync("testCustomerId");

            _customerProfileRepository.Verify();
            Assert.Equal(CustomerProfileErrorCodes.None, actual.ErrorCode);
        }

        [Fact]
        public async Task TryingToGetCustomerProfileByCustomerId_InvalidCustomerId_ErrorCodeIsReturned()
        {
            _customerProfileRepository
                .Setup(x => x.GetByCustomerIdAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<TransactionContext>()))
                .ReturnsAsync((ICustomerProfile)null);

            var customerProfileService = CreateSutInstance();

            var actual = await customerProfileService.GetByCustomerIdAsync("testCustomerId");

            Assert.Equal(CustomerProfileErrorCodes.CustomerProfileDoesNotExist, actual.ErrorCode);
            Assert.Null(actual.Profile);
        }

        [Fact]
        public async Task TryingToDeleteCustomerProfileByCustomerId_ValidCustomerId_SuccessfullyDeleted()
        {
            _customerProfileRepository
                .Setup(x => x.DeleteAsync(It.IsAny<string>()))
                .ReturnsAsync(true)
                .Verifiable();

            var customerProfileService = CreateSutInstance();

            await customerProfileService.RemoveAsync("testCustomerId");

            _customerProfileRepository.Verify();
        }

        [Fact]
        public async Task TryingToGetPaginatedCustomerProfiles_EverythingIsValid_SuccessfullyReturned()
        {
            var customerProfilePaginatedResult = new List<ICustomerProfile> { new CustomerProfileModel(), };
            _customerProfileRepository
                .Setup(x => x.GetPaginatedAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(customerProfilePaginatedResult)
                .Verifiable();

            var customerProfileService = CreateSutInstance();

            var actual = await customerProfileService.GetPaginatedAsync(1, 5);

            _customerProfileRepository.Verify();
            Assert.NotNull(actual.Customers);
            Assert.True(actual.Customers.Any());
        }

        [Fact]
        public async Task TryingToGetPaginatedCustomerProfiles_CurrentPageIsInvalid_ArgumentExceptionIsThrown()
        {
            var customerProfileService = CreateSutInstance();

            await Assert.ThrowsAsync<ArgumentException>(() => customerProfileService.GetPaginatedAsync(-1, 10));
        }

        [Fact]
        public async Task TryingToGetPaginatedCustomerProfiles_PageSizeIsInvalid_ArgumentExceptionIsThrown()
        {
            var customerProfileService = CreateSutInstance();

            await Assert.ThrowsAsync<ArgumentException>(() => customerProfileService.GetPaginatedAsync(1, 0));
        }

        [Fact]
        public async Task TryingToGetCustomerProfileByEmail_ValidCustomerEmail_SuccessfullyReturned()
        {
            var customer = new CustomerProfileModel();

            _customerProfileRepository
                .Setup(x => x.GetByCustomerEmailAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(customer)
                .Verifiable();

            var customerProfileService = CreateSutInstance();

            var actual = await customerProfileService.GetByEmailAsync("testCustomerEmail");

            _customerProfileRepository.Verify();

            Assert.Equal(CustomerProfileErrorCodes.None, actual.ErrorCode);
            Assert.NotNull(actual.Profile);
        }

        [Fact]
        public async Task TryingToGetCustomerProfileByEmail_InvalidCustomerEmail_ErrorCodeIsReturned()
        {
            _customerProfileRepository
                .Setup(x => x.GetByCustomerEmailAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync((ICustomerProfile)null)
                .Verifiable();

            var customerProfileService = CreateSutInstance();

            var actual = await customerProfileService.GetByEmailAsync("testCustomerEmail");

            _customerProfileRepository.Verify();

            Assert.Equal(CustomerProfileErrorCodes.CustomerProfileDoesNotExist, actual.ErrorCode);
            Assert.Null(actual.Profile);
        }

        [Fact]
        public async Task TryingToSetEmailAsVerified_ValidCustomerId_SuccessfullyReturned()
        {
            var customerProfileModel = new CustomerProfileModel
            {
                IsEmailVerified = true
            };
            _customerProfileRepository
                .Setup(x => x.GetByCustomerIdAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<TransactionContext>()))
                .ReturnsAsync(customerProfileModel)
                .Verifiable();
            var customerProfileResult = CustomerProfileErrorCodes.None;
            _customerProfileRepository
                .Setup(x => x.SetEmailVerifiedAsync("testCustomerId"))
                .ReturnsAsync((customerProfileResult, false))
                .Verifiable();

            var customerProfileService = CreateSutInstance();

            var result = await customerProfileService.SetEmailAsVerifiedAsync("testCustomerId");
            var actual = await customerProfileService.GetByCustomerIdAsync("testCustomerId");

            _customerProfileRepository.Verify();
            Assert.True(actual.Profile.IsEmailVerified);
            Assert.Equal(CustomerProfileErrorCodes.None, result);
        }

        [Fact]
        public async Task TryingToSetPhoneAsVerified_CustomerProfileDoesNotExist_CustomerProfileDoesNotExistReturned()
        {
            var customerProfileResult = CustomerProfileErrorCodes.CustomerProfileDoesNotExist;
            _customerProfileRepository
                .Setup(x => x.SetPhoneAsVerifiedAsync("testCustomerId"))
                .ReturnsAsync((customerProfileResult, false))
                .Verifiable();

            var customerProfileService = CreateSutInstance();

            var result = await customerProfileService.SetPhoneAsVerifiedAsync("testCustomerId");

            Assert.Equal(CustomerProfileErrorCodes.CustomerProfileDoesNotExist, result);
        }

        [Fact]
        public async Task TryingToSetPhoneAsVerified_CustomerPhoneMissing_CustomerProfilePhoneIsMissingReturned()
        {
            var customerProfileModel = new CustomerProfileModel
            {
                IsPhoneVerified = false,
            };
            _customerProfileRepository
                .Setup(x => x.GetByCustomerIdAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<TransactionContext>()))
                .ReturnsAsync(customerProfileModel);
            var customerProfileResult = CustomerProfileErrorCodes.CustomerProfilePhoneIsMissing;
            _customerProfileRepository
                .Setup(x => x.SetPhoneAsVerifiedAsync("testCustomerId"))
                .ReturnsAsync((customerProfileResult, false));

            var customerProfileService = CreateSutInstance();

            var result = await customerProfileService.SetPhoneAsVerifiedAsync("testCustomerId");

            Assert.Equal(CustomerProfileErrorCodes.CustomerProfilePhoneIsMissing, result);
        }

        [Fact]
        public async Task TryingToSetPhoneAsVerified_AnotherCustomerWithTheSameVerifiedPhone_PhoneAlreadyExistsReturned()
        {
            var customerProfileModel = new CustomerProfileModel
            {
                IsPhoneVerified = true,
                PhoneNumber = "12345678"
            };
            _customerProfileRepository
                .Setup(x => x.GetByCustomerIdAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<TransactionContext>()))
                .ReturnsAsync(customerProfileModel)
                .Verifiable();
            _customerProfileRepository
                .Setup(x => x.IsPhoneNumberUsedByAnotherCustomer(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);
            var customerProfileResult = CustomerProfileErrorCodes.CustomerProfilePhoneAlreadyVerified;
            _customerProfileRepository
                .Setup(x => x.SetPhoneAsVerifiedAsync("testCustomerId"))
                .ReturnsAsync((customerProfileResult, false))
                .Verifiable();

            var customerProfileService = CreateSutInstance();

            var result = await customerProfileService.SetPhoneAsVerifiedAsync("testCustomerId");

            Assert.Equal(CustomerProfileErrorCodes.PhoneAlreadyExists, result);
        }

        [Fact]
        public async Task TryingToSetPhoneAsVerified_AlreadyVerifiedCustomerPhone_CustomerProfilePhoneAlreadyVerifiedReturned()
        {
            var customerProfileModel = new CustomerProfileModel
            {
                IsPhoneVerified = true,
                PhoneNumber = "12345678"
            };
            _customerProfileRepository
                .Setup(x => x.GetByCustomerIdAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<TransactionContext>()))
                .ReturnsAsync(customerProfileModel)
                .Verifiable();
            _customerProfileRepository
                .Setup(x => x.IsPhoneNumberUsedByAnotherCustomer(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(false);
            var customerProfileResult = CustomerProfileErrorCodes.CustomerProfilePhoneAlreadyVerified;
            _customerProfileRepository
                .Setup(x => x.SetPhoneAsVerifiedAsync("testCustomerId"))
                .ReturnsAsync((customerProfileResult, false))
                .Verifiable();

            var customerProfileService = CreateSutInstance();

            var result = await customerProfileService.SetPhoneAsVerifiedAsync("testCustomerId");
            var actual = await customerProfileService.GetByCustomerIdAsync("testCustomerId");

            _customerProfileRepository.Verify();
            Assert.True(actual.Profile.IsPhoneVerified);
            Assert.Equal(CustomerProfileErrorCodes.CustomerProfilePhoneAlreadyVerified, result);
        }

        [Fact]
        public async Task TryingToSetPhoneAsVerified_ValidCustomerId_SuccessfullyReturned()
        {
            var customerProfileModel = new CustomerProfileModel
            {
                IsPhoneVerified = true,
                PhoneNumber = "12345678S"
            };
            _customerProfileRepository
                .Setup(x => x.GetByCustomerIdAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<TransactionContext>()))
                .ReturnsAsync(customerProfileModel)
                .Verifiable();
            _customerProfileRepository
                .Setup(x => x.IsPhoneNumberUsedByAnotherCustomer(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(false);
            var customerProfileResult = CustomerProfileErrorCodes.None;
            _customerProfileRepository
                .Setup(x => x.SetPhoneAsVerifiedAsync("testCustomerId"))
                .ReturnsAsync((customerProfileResult, false))
                .Verifiable();

            var customerProfileService = CreateSutInstance();

            var result = await customerProfileService.SetPhoneAsVerifiedAsync("testCustomerId");
            var actual = await customerProfileService.GetByCustomerIdAsync("testCustomerId");

            _customerProfileRepository.Verify();
            Assert.True(actual.Profile.IsPhoneVerified);
            Assert.Equal(CustomerProfileErrorCodes.None, result);
        }


        [Fact]
        public async Task TryingToSetEmailAsVerified_AlreadyVerifiedCustomerEmail_CustomerProfileEmailAlreadyVerifiedReturned()
        {
            var customerProfileModel = new CustomerProfileModel
            {
                IsEmailVerified = true
            };
            _customerProfileRepository
                .Setup(x => x.GetByCustomerIdAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<TransactionContext>()))
                .ReturnsAsync(customerProfileModel)
                .Verifiable();
            var customerProfileResult = CustomerProfileErrorCodes.CustomerProfileEmailAlreadyVerified;
            _customerProfileRepository
                .Setup(x => x.SetEmailVerifiedAsync("testCustomerId"))
                .ReturnsAsync((customerProfileResult, false))
                .Verifiable();

            var customerProfileService = CreateSutInstance();

            var result = await customerProfileService.SetEmailAsVerifiedAsync("testCustomerId");
            var actual = await customerProfileService.GetByCustomerIdAsync("testCustomerId");

            _customerProfileRepository.Verify();
            Assert.True(actual.Profile.IsEmailVerified);
            Assert.Equal(CustomerProfileErrorCodes.CustomerProfileEmailAlreadyVerified, result);
        }

        [Fact]
        public async Task SetPhoneInfoAsync_CustomerProfileDoesNotExist_CustomerProfileDoesNotExistReturned()
        {
            _customerProfileRepository
                .Setup(x => x.GetByCustomerIdAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<TransactionContext>()))
                .ReturnsAsync((ICustomerProfile)null)
                .Verifiable();

            var customerProfileService = CreateSutInstance();

            var result = await customerProfileService.UpdatePhoneInfoAsync("testCustomerId", "+12345678", 0);

            Assert.Equal(CustomerProfileErrorCodes.CustomerProfileDoesNotExist, result);
        }

        [Fact]
        public async Task SetPhoneInfoAsync_CountryPhoneCodeDoesNotExist_ErrorReturned()
        {
            _customerProfileRepository
                .Setup(x => x.GetByCustomerIdAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<TransactionContext>()))
                .ReturnsAsync(new CustomerProfileModel());

            _customerProfileRepository
                .Setup(x => x.UpdatePhoneInfoAsync("testCustomerId", null, "12345678", 0, false))
                .ReturnsAsync(CustomerProfileErrorCodes.None);

            _dictionariesClientMock.Setup(x => x.Salesforce.GetCountryPhoneCodeByIdAsync(0))
                .ReturnsAsync((CountryPhoneCodeModel)null);

            var customerProfileService = CreateSutInstance();

            var result = await customerProfileService.UpdatePhoneInfoAsync("testCustomerId", "12345678", 0);

            Assert.Equal(CustomerProfileErrorCodes.InvalidCountryPhoneCodeId, result);
        }

        [Fact]
        public async Task SetPhoneInfoAsync_AnotherCustomerWithTheSameVerifiedPhone_PhoneAlreadyExistsErrorReturned()
        {
            var customerId = "testCustomerId";
            var phone = "123456789";
            var fullPhone = "+123456789";

            _customerProfileRepository
                .Setup(x => x.GetByCustomerIdAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<TransactionContext>()))
                .ReturnsAsync(new CustomerProfileModel
                {
                    ShortPhoneNumber = phone,
                    PhoneNumber = fullPhone,
                    CountryPhoneCodeId = 1
                });

            _customerProfileRepository
                .Setup(x => x.IsPhoneNumberUsedByAnotherCustomer(customerId, fullPhone))
                .ReturnsAsync(true);

            _dictionariesClientMock.Setup(x => x.Salesforce.GetCountryPhoneCodeByIdAsync(1))
                .ReturnsAsync(new CountryPhoneCodeModel { IsoCode = "iso" });

            var customerProfileService = CreateSutInstance();

            var result = await customerProfileService.UpdatePhoneInfoAsync(customerId, phone, 1);

            Assert.Equal(CustomerProfileErrorCodes.PhoneAlreadyExists, result);
        }

        [Fact]
        public async Task SetPhoneInfoAsync_E164PhoneNumberIsNull_InvalidPhoneErrorReturned()
        {
            var customerId = "testCustomerId";
            var phone = "00";

            _customerProfileRepository
                .Setup(x => x.GetByCustomerIdAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<TransactionContext>()))
                .ReturnsAsync(new CustomerProfileModel
                {
                    CountryPhoneCodeId = 1
                });

            _dictionariesClientMock.Setup(x => x.Salesforce.GetCountryPhoneCodeByIdAsync(1))
                .ReturnsAsync(new CountryPhoneCodeModel { IsoCode = "iso" });

            var customerProfileService = CreateSutInstance();

            var result = await customerProfileService.UpdatePhoneInfoAsync(customerId, phone, 1);

            Assert.Equal(CustomerProfileErrorCodes.InvalidPhoneNumber, result);
        }

        [Fact]
        public async Task SetPhoneInfoAsync_PhoneInfoIsTheSameAsBefore_RepoNotCalled()
        {
            var customerId = "testCustomerId";
            var phone = "123456789";
            var fullPhone = "+123456789";

            _customerProfileRepository
                .Setup(x => x.GetByCustomerIdAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<TransactionContext>()))
                .ReturnsAsync(new CustomerProfileModel
                {
                    ShortPhoneNumber = phone,
                    PhoneNumber = fullPhone,
                    CountryPhoneCodeId = 1
                });

            _customerProfileRepository
                .Setup(x => x.UpdatePhoneInfoAsync(customerId, fullPhone, phone, 1, false))
                .ReturnsAsync(CustomerProfileErrorCodes.None);

            _dictionariesClientMock.Setup(x => x.Salesforce.GetCountryPhoneCodeByIdAsync(1))
                .ReturnsAsync(new CountryPhoneCodeModel { IsoCode = "iso" });

            var customerProfileService = CreateSutInstance();

            var result = await customerProfileService.UpdatePhoneInfoAsync(customerId, phone, 1);

            Assert.Equal(CustomerProfileErrorCodes.None, result);
            _customerProfileRepository.Verify(
                x => x.UpdatePhoneInfoAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<int>(), false), Times.Never);
        }

        [Fact]
        public async Task SetPhoneInfoAsync_SuccessfullyUpdated()
        {
            _customerProfileRepository
                .Setup(
                    x => x.GetByCustomerIdAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<TransactionContext>()))
                .ReturnsAsync(new CustomerProfileModel {CountryPhoneCodeId = 1});

            _customerProfileRepository
                .Setup(x => x.UpdatePhoneInfoAsync("testCustomerId", It.IsAny<string>(),It.IsAny<string>(), 1, false))
                .ReturnsAsync(CustomerProfileErrorCodes.None);

            _dictionariesClientMock.Setup(x => x.Salesforce.GetCountryPhoneCodeByIdAsync(1))
                .ReturnsAsync(new CountryPhoneCodeModel {IsoCode = "iso"});

            var customerProfileService = CreateSutInstance();

            var result = await customerProfileService.UpdatePhoneInfoAsync("testCustomerId", "+12345678", 1);

            Assert.Equal(CustomerProfileErrorCodes.None, result);
        }

        private CustomerProfileService CreateSutInstance()
        {
            return new CustomerProfileService(
                _customerProfileRepository.Object,
                _dictionariesClientMock.Object,
                _emailVerifiedPublisher.Object,
                _phoneVerifiedPublisher.Object,
                _deactivationRequestPublisher.Object,
                new SqlContextFactoryFake<CustomerProfileContext>(x => new CustomerProfileContext(x)),
                EmptyLogFactory.Instance);
        }
    }
}
