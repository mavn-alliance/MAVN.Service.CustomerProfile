using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Common.Log;
using Falcon.Common;
using Falcon.Common.Encryption;
using Lykke.Common.Log;
using Lykke.Common.MsSql;
using Lykke.Service.CustomerProfile.Domain.Enums;
using Lykke.Service.CustomerProfile.Domain.Models;
using Lykke.Service.CustomerProfile.Domain.Repositories;
using Lykke.Service.CustomerProfile.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lykke.Service.CustomerProfile.MsSqlRepositories.Repositories
{
    public class CustomerProfileRepository : ICustomerProfileRepository
    {
        private readonly MsSqlContextFactory<CustomerProfileContext> _contextFactory;
        private readonly IEncryptionService _encryptionService;
        private readonly ILog _log;

        public CustomerProfileRepository(
            MsSqlContextFactory<CustomerProfileContext> contextFactory,
            IEncryptionService encryptionService,
            ILogFactory logFactory)
        {
            _contextFactory = contextFactory;
            _encryptionService = encryptionService;
            _log = logFactory.CreateLog(this);
        }

        public async Task<CustomerProfileErrorCodes> CreateIfNotExistAsync(ICustomerProfile customerProfile)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var lowerCasedEmail = _encryptionService.EncryptValue(customerProfile.Email.ToLower());
                
                //Note: Here we pull only customers that are Active (Not Archived/Deleted) and if there are none we create a new profile.
                //      We might want to give option for Profile Restore instead of creating new one if the person had a profile previously.
                var existentCustomerProfile = await context.CustomerProfiles
                    .Include(x => x.LoginProviders)
                    .IgnoreQueryFilters()
                    .FirstOrDefaultAsync(c =>
                        c.CustomerId == customerProfile.CustomerId
                        || c.Email == customerProfile.Email
                        || c.LowerCasedEmail == lowerCasedEmail);

                if (existentCustomerProfile != null)
                {
                    var registrationProvider = customerProfile.LoginProviders.First();
                    var hasDifferentProvider = existentCustomerProfile.LoginProviders
                        .Any(x => x.LoginProvider != registrationProvider);

                    return hasDifferentProvider ? 
                        CustomerProfileErrorCodes.CustomerProfileAlreadyExistsWithDifferentProvider :
                        CustomerProfileErrorCodes.CustomerProfileAlreadyExists;
                }

                var entity = CustomerProfileEntity.Create(customerProfile);

                entity = _encryptionService.Encrypt(entity);

                context.CustomerProfiles.Add(entity);

                await context.SaveChangesAsync();

                return CustomerProfileErrorCodes.None;
            }
        }

        public async Task<CustomerProfileErrorCodes> UpdateAsync(string customerId, string firstName, string lastName,
            string phoneNumber, string shortPhoneNumber, int countryPhoneCodeId, int countryOfResidenceId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.CustomerProfiles.FirstOrDefaultAsync(o => o.CustomerId == customerId);

                if (entity == null)
                    return CustomerProfileErrorCodes.CustomerProfileDoesNotExist;

                entity = _encryptionService.Decrypt(entity);

                entity.FirstName = firstName;
                entity.LastName = lastName;
                entity.PhoneNumber = phoneNumber;
                entity.ShortPhoneNumber = shortPhoneNumber;
                entity.CountryPhoneCodeId = countryPhoneCodeId;
                entity.CountryOfResidenceId = countryOfResidenceId;

                entity = _encryptionService.Encrypt(entity);

                context.CustomerProfiles.Update(entity);

                await context.SaveChangesAsync();

                return CustomerProfileErrorCodes.None;
            }
        }

        public async Task<CustomerProfileErrorCodes> UpdateTierAsync(string customerId, string tierId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.CustomerProfiles.FirstOrDefaultAsync(o => o.CustomerId == customerId);

                if (entity == null)
                    return CustomerProfileErrorCodes.CustomerProfileDoesNotExist;

                entity.TierId = tierId;

                context.CustomerProfiles.Update(entity);

                await context.SaveChangesAsync();

                return CustomerProfileErrorCodes.None;
            }
        }

        public async Task<CustomerProfileErrorCodes> UpdatePhoneInfoAsync
            (string customerId, string phoneNumber, string shortPhoneNumber, int countryPhoneCodeId, bool isPhoneVerified)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.CustomerProfiles
                    .IgnoreQueryFilters()
                    .FirstOrDefaultAsync(o => o.CustomerId == customerId);

                if (entity == null)
                    return CustomerProfileErrorCodes.CustomerProfileDoesNotExist;

                entity = _encryptionService.Decrypt(entity);

                entity.CountryPhoneCodeId = countryPhoneCodeId;
                entity.PhoneNumber = phoneNumber;
                entity.ShortPhoneNumber = shortPhoneNumber;
                entity.IsPhoneVerified = isPhoneVerified;

                entity = _encryptionService.Encrypt(entity);

                context.CustomerProfiles.Update(entity);

                await context.SaveChangesAsync();

                return CustomerProfileErrorCodes.None;
            }
        }

        public async Task<CustomerProfileErrorCodes> UpdateEmailAsync(string customerId, string email, bool isEmailVerified)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var entity = await context.CustomerProfiles
                        .IgnoreQueryFilters()
                        .FirstOrDefaultAsync(o => o.CustomerId == customerId);

                    if (entity == null)
                        return CustomerProfileErrorCodes.CustomerProfileDoesNotExist;

                    var encryptedEmail = _encryptionService.EncryptValue(email);
                    var existingEntity = await context.CustomerProfiles.FirstOrDefaultAsync(o => o.Email == encryptedEmail);
                    if (existingEntity != null)
                        return CustomerProfileErrorCodes.CustomerProfileAlreadyExists;

                    entity = _encryptionService.Decrypt(entity);

                    var archiveEntity = CustomerProfileArchiveEntity.Create(entity);
                    archiveEntity.CustomerId = $"{archiveEntity.CustomerId}_{DateTime.UtcNow:yyMMddHHmmss}";
                    archiveEntity = _encryptionService.Encrypt(archiveEntity);
                    context.CustomerProfilesArchive.Add(archiveEntity);

                    entity.Email = email;
                    entity.LowerCasedEmail = email.ToLower();
                    entity.IsEmailVerified = isEmailVerified;
                    entity = _encryptionService.Encrypt(entity);
                    context.CustomerProfiles.Update(entity);

                    await context.SaveChangesAsync();

                    transaction.Commit();

                    return CustomerProfileErrorCodes.None;
                }
            }
        }

        public Task<int> GetTotalByDateAsync(DateTime date, bool includeNotVerified = false)
        {
            return GetTotalAsync(c => c.Registered < date, includeNotVerified);
        }

        public async Task<int> GetTotalAsync(bool includeNotVerified = false, bool includeNotActive = false)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var baseQuery = includeNotVerified
                    ?  context.CustomerProfiles.IgnoreQueryFilters()
                    :  context.CustomerProfiles;

                if (!includeNotActive)
                    baseQuery = baseQuery.Where(c => c.Status == CustomerProfileStatus.Active);

                return await baseQuery.CountAsync();
            }
        }

        public async Task<int> GetByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                return await context.CustomerProfiles
                    .Where(c => c.Registered >= startDate && c.Registered < endDate)
                    .CountAsync();
            }
        }

        public async Task<ICustomerProfile> GetByCustomerIdAsync(string customerId, bool includeNotVerified = false, bool includeNotActive = false, TransactionContext txContext = null)
        {
            using (var context = _contextFactory.CreateDataContext(txContext))
            {
                var result = includeNotVerified
                    ? await context.CustomerProfiles
                        .Include(x => x.LoginProviders)
                        .IgnoreQueryFilters()
                        .Where(c => includeNotActive || c.Status == CustomerProfileStatus.Active)
                        .FirstOrDefaultAsync(c => c.CustomerId == customerId)
                    : await context.CustomerProfiles
                        .Include(x => x.LoginProviders)
                        .Where(c => includeNotActive || c.Status == CustomerProfileStatus.Active)
                        .FirstOrDefaultAsync(c => c.CustomerId == customerId);

                if (result == null)
                    return null;

                result = _encryptionService.Decrypt(result);

                return new CustomerProfileModel
                {
                    CustomerId = result.CustomerId,
                    Registered = result.Registered,
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    Email = result.Email,
                    IsEmailVerified = result.IsEmailVerified,
                    IsPhoneVerified = result.IsPhoneVerified,
                    PhoneNumber = result.PhoneNumber,
                    ShortPhoneNumber = result.ShortPhoneNumber,
                    CountryPhoneCodeId = result.CountryPhoneCodeId,
                    CountryOfResidenceId = result.CountryOfResidenceId,
                    CountryOfNationalityId = result.CountryOfNationalityId,
                    TierId = result.TierId,
                    LoginProviders = new List<LoginProvider>(
                        result.LoginProviders.Select(x => x.LoginProvider)),
                    Status = result.Status,
                };
            }
        }

        public async Task<IEnumerable<ICustomerProfile>> GetPaginatedAsync(int skip, int take, bool includeNotVerified = false, bool includeNotActive = false)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var baseQuery = includeNotVerified
                    ? context.CustomerProfiles
                        .IgnoreQueryFilters()
                    : context.CustomerProfiles;
                
                if(!includeNotActive)
                 baseQuery = baseQuery.Where(c => c.Status == CustomerProfileStatus.Active);
                
                var customers = await baseQuery
                    .OrderByDescending(x => x.Registered)
                    .Skip(skip)
                    .Take(take)
                    .Include(c => c.LoginProviders)
                    .Select(c => _encryptionService.Decrypt(c))
                    .Select(_selectExpression)
                    .ToArrayAsync();

                return customers;
            }
        }

        public async Task<bool> DeleteAsync(string customerId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.CustomerProfiles
                    .IgnoreQueryFilters()
                    .FirstOrDefaultAsync(c => c.CustomerId == customerId);

                if (entity == null)
                    return false;

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var archiveEntity = CustomerProfileArchiveEntity.Create(entity);

                        context.CustomerProfilesArchive.Add(archiveEntity);

                        context.CustomerProfiles.Remove(entity);

                        await context.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        _log.Error(e, "Error occured while deleting customer profile", $"customerId = {customerId}");
                        return false;
                    }
                }
            }

            return true;
        }

        public async Task<ICustomerProfile> GetByCustomerEmailAsync(string email, bool includeNotVerified = false, bool includeNotActive = false)
        {
            var encryptedEmail = _encryptionService.EncryptValue(email);
            var encryptedLowercasedEmail = _encryptionService.EncryptValue(email.ToLower());

            using (var context = _contextFactory.CreateDataContext())
            {
                var result = includeNotVerified
                    ? await context.CustomerProfiles
                        .Include(c => c.LoginProviders)
                        .IgnoreQueryFilters()
                        .Where(c => includeNotActive || c.Status == CustomerProfileStatus.Active)
                        .FirstOrDefaultAsync(c => c.Email == encryptedEmail || c.LowerCasedEmail == encryptedLowercasedEmail)
                    : await context.CustomerProfiles
                        .Include(c => c.LoginProviders)
                        .Where(c => includeNotActive || c.Status == CustomerProfileStatus.Active)
                        .FirstOrDefaultAsync(c => c.Email == encryptedEmail || c.LowerCasedEmail == encryptedLowercasedEmail);

                if (result == null)
                    return null;

                result = _encryptionService.Decrypt(result);

                return new CustomerProfileModel
                {
                    CustomerId = result.CustomerId,
                    Registered = result.Registered,
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    Email = result.Email,
                    IsEmailVerified = result.IsEmailVerified,
                    IsPhoneVerified = result.IsPhoneVerified,
                    PhoneNumber = result.PhoneNumber,
                    ShortPhoneNumber = result.ShortPhoneNumber,
                    CountryPhoneCodeId = result.CountryPhoneCodeId,
                    CountryOfResidenceId = result.CountryOfResidenceId,
                    CountryOfNationalityId = result.CountryOfNationalityId,
                    TierId = result.TierId,
                    LoginProviders = new List<LoginProvider>(
                        result.LoginProviders.Select(x => x.LoginProvider)),
                    Status = result.Status,
                };
            }
        }

        public async Task<ICustomerProfile> GetByCustomerPhoneAsync(string phone, bool includeNotVerified = false, bool includeNotActive = false)
        {
            var e164FormattedNumber = PhoneUtils.GetE164FormattedNumber(phone);

            if (string.IsNullOrEmpty(e164FormattedNumber))
            {
                _log.Warning("Invalid phone number. Failed to convert to E164 format", null, phone);
                return null;
            }

            var encryptedPhone = _encryptionService.EncryptValue(e164FormattedNumber);

            using (var context = _contextFactory.CreateDataContext())
            {
                var result = includeNotVerified
                    ? await context.CustomerProfiles
                        .Include(c => c.LoginProviders)
                        .IgnoreQueryFilters()
                        .Where(c => includeNotActive || c.Status == CustomerProfileStatus.Active)
                        .FirstOrDefaultAsync(c => c.PhoneNumber == encryptedPhone)
                    : await context.CustomerProfiles
                        .Include(c => c.LoginProviders)
                        .Where(c => includeNotActive || c.Status == CustomerProfileStatus.Active)
                        .FirstOrDefaultAsync(c => c.PhoneNumber == encryptedPhone);

                if (result == null)
                    return null;

                result = _encryptionService.Decrypt(result);

                return new CustomerProfileModel
                {
                    CustomerId = result.CustomerId,
                    Registered = result.Registered,
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    Email = result.Email,
                    IsEmailVerified = result.IsEmailVerified,
                    PhoneNumber = result.PhoneNumber,
                    ShortPhoneNumber = result.ShortPhoneNumber,
                    CountryPhoneCodeId = result.CountryPhoneCodeId,
                    CountryOfResidenceId = result.CountryOfResidenceId,
                    CountryOfNationalityId = result.CountryOfNationalityId,
                    TierId = result.TierId,
                    IsPhoneVerified = result.IsPhoneVerified,
                    LoginProviders = new List<LoginProvider>(
                        result.LoginProviders.Select(x => x.LoginProvider)),
                    Status = result.Status,
                };
            }
        }

        public async Task<(CustomerProfileErrorCodes error, bool wasVerfiedBefore)> SetEmailVerifiedAsync(string customerId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.CustomerProfiles
                    .IgnoreQueryFilters()
                    .FirstOrDefaultAsync(c => c.CustomerId == customerId);

                if (entity == null)
                    return (CustomerProfileErrorCodes.CustomerProfileDoesNotExist, false);
                
                var wasEmailPreviouslyVerified = entity.WasEmailEverVerified;

                if (entity.IsEmailVerified)
                    return (CustomerProfileErrorCodes.CustomerProfileEmailAlreadyVerified, wasEmailPreviouslyVerified);

                entity.IsEmailVerified = true;
                entity.WasEmailEverVerified = true;

                await context.SaveChangesAsync();

                return (CustomerProfileErrorCodes.None, wasEmailPreviouslyVerified);
            }
        }

        public async Task<(CustomerProfileErrorCodes error, bool wasVerfiedBefore)> SetPhoneAsVerifiedAsync(string customerId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.CustomerProfiles
                    .IgnoreQueryFilters()
                    .FirstOrDefaultAsync(c => c.CustomerId == customerId);

                if (entity == null)
                    return (CustomerProfileErrorCodes.CustomerProfileDoesNotExist, false);

                var wasPhonePreviouslyVerified = entity.WasPhoneEverVerified;

                if (entity.IsPhoneVerified)
                    return (CustomerProfileErrorCodes.CustomerProfilePhoneAlreadyVerified, wasPhonePreviouslyVerified);

                entity.IsPhoneVerified = true;
                entity.WasPhoneEverVerified = true;

                await context.SaveChangesAsync();

                return (CustomerProfileErrorCodes.None, wasPhonePreviouslyVerified);
            }
        }

        public async Task<IEnumerable<ICustomerProfile>> GetByIdsAsync(IEnumerable<string> ids, bool includeNotVerified = false, bool includeNotActive = false)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var baseQuery = includeNotVerified
                    ? context.CustomerProfiles
                        .Include(c => c.LoginProviders)
                        .IgnoreQueryFilters()
                        .Where(c => ids.Contains(c.CustomerId))
                    : context.CustomerProfiles
                        .Include(c => c.LoginProviders)
                        .Where(c => ids.Contains(c.CustomerId));

                if (!includeNotActive)
                    baseQuery = baseQuery.Where(c => c.Status == CustomerProfileStatus.Active);

                var customers = await baseQuery
                    .Select(c => _encryptionService.Decrypt(c))
                    .Select(_selectExpression)
                    .ToArrayAsync();

                return customers;
            }
        }

        public async Task<bool> IsPhoneNumberUsedByAnotherCustomer(string customerId, string fullPhoneNumber)
        {
            var encryptedPhone = _encryptionService.EncryptValue(fullPhoneNumber);

            using (var context = _contextFactory.CreateDataContext())
            {
                var result = await context.CustomerProfiles
                    .IgnoreQueryFilters()
                    .AnyAsync(c => c.CustomerId != customerId && c.PhoneNumber == encryptedPhone && c.IsPhoneVerified);

                return result;
            }
        }

        public async Task ChangeProfileStatus(string customerId, CustomerProfileStatus status, TransactionContext txContext = null)
        {
            using (var context = _contextFactory.CreateDataContext(txContext))
            {
                var entity = new CustomerProfileEntity{ CustomerId = customerId };

                context.CustomerProfiles.Attach(entity);

                entity.Status = status;

                await context.SaveChangesAsync();
            }
        }

        private async Task<int> GetTotalAsync(Func<CustomerProfileEntity, bool> filter, bool includeNotVerified)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var baseQuery = includeNotVerified
                    ? context.CustomerProfiles.IgnoreQueryFilters()
                    : context.CustomerProfiles;
                
                if (filter == null)
                    return await baseQuery.CountAsync();

                return baseQuery.Where(filter).Count();
            }
        }

        private readonly Expression<Func<CustomerProfileEntity, CustomerProfileModel>> _selectExpression =
            entity => new CustomerProfileModel
            {
                CustomerId = entity.CustomerId,
                Email = entity.Email,
                FirstName = entity.FirstName,
                IsEmailVerified = entity.IsEmailVerified,
                LastName = entity.LastName,
                PhoneNumber = entity.PhoneNumber,
                Registered = entity.Registered,
                ShortPhoneNumber = entity.ShortPhoneNumber,
                CountryPhoneCodeId = entity.CountryPhoneCodeId,
                CountryOfResidenceId = entity.CountryOfResidenceId,
                CountryOfNationalityId = entity.CountryOfNationalityId,
                TierId = entity.TierId,
                IsPhoneVerified = entity.IsPhoneVerified,
                LoginProviders = new List<LoginProvider>(
                    entity.LoginProviders.Select(x => x.LoginProvider)),
                Status = entity.Status,
            };
    }
}
