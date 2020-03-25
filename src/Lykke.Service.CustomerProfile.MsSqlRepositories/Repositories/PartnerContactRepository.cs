using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Common.Log;
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
    public class PartnerContactRepository : IPartnerContactRepository
    {
        private readonly MsSqlContextFactory<CustomerProfileContext> _contextFactory;
        private readonly IEncryptionService _encryptionService;
        private readonly ILog _log;

        public PartnerContactRepository(
            MsSqlContextFactory<CustomerProfileContext> contextFactory,
            IEncryptionService encryptionService,
            ILogFactory logFactory)
        {
            _contextFactory = contextFactory;
            _encryptionService = encryptionService;
            _log = logFactory.CreateLog(this);
        }

        public async Task<IPartnerContact> GetByLocationIdAsync(string locationId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var result = await context.PartnerContacts
                    .FirstOrDefaultAsync(c => c.LocationId == locationId);

                if (result == null)
                    return null;

                result = _encryptionService.Decrypt(result);

                return new PartnerContactModel
                {
                    LocationId = result.LocationId,
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    Email = result.Email,
                    PhoneNumber = result.PhoneNumber
                };
            }
        }

        public async Task<IPartnerContact> GetByEmailAsync(string email)
        {
            var encryptedEmail = _encryptionService.EncryptValue(email);

            using (var context = _contextFactory.CreateDataContext())
            {
                var result = await context.PartnerContacts
                    .FirstOrDefaultAsync(c => c.Email == encryptedEmail);

                if (result == null)
                    return null;

                result = _encryptionService.Decrypt(result);

                return new PartnerContactModel
                {
                    LocationId = result.LocationId,
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    Email = result.Email,
                    PhoneNumber = result.PhoneNumber
                };
            }
        }

        public async Task<IPartnerContact> GetByPhoneAsync(string phone)
        {
            var encryptedPhone = _encryptionService.EncryptValue(phone);

            using (var context = _contextFactory.CreateDataContext())
            {
                var result = await context.PartnerContacts
                    .FirstOrDefaultAsync(c => c.PhoneNumber == encryptedPhone);

                if (result == null)
                    return null;

                result = _encryptionService.Decrypt(result);

                return new PartnerContactModel
                {
                    LocationId = result.LocationId,
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    Email = result.Email,
                    PhoneNumber = result.PhoneNumber
                };
            }
        }

        public async Task<bool> DeleteAsync(string locationId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var entity = await context.PartnerContacts
                            .FirstOrDefaultAsync(c => c.LocationId == locationId);

                        if (entity == null)
                            return false;

                        var archiveEntity = PartnerContactArchiveEntity.Create(entity);

                        context.PartnerContactsArchive.Add(archiveEntity);

                        context.PartnerContacts.Remove(entity);

                        await context.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        _log.Error(e, "Error occured while deleting partner contact ", $"locationId = {locationId}");
                        return false;
                    }
                }
            }

            return true;
        }

        public async Task<IEnumerable<IPartnerContact>> GetPaginatedAsync(int skip, int take)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var partners = await context.PartnerContacts
                    .Skip(skip)
                    .Take(take)
                    .Select(c => _encryptionService.Decrypt(c))
                    .Select(_selectExpression)
                    .ToArrayAsync();

                return partners;
            }
        }

        public async Task<int> GetTotalAsync()
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                return await context.PartnerContacts.CountAsync();
            }
        }

        public async Task<PartnerContactErrorCodes> CreateIfNotExistAsync(PartnerContactModel partnerContact)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var existentPartnerContact = await context.PartnerContacts
                    .FirstOrDefaultAsync(c => c.LocationId == partnerContact.LocationId);

                if (existentPartnerContact != null)
                {
                    return PartnerContactErrorCodes.PartnerContactAlreadyExists;
                }

                var entity = PartnerContactEntity.Create(partnerContact);

                entity = _encryptionService.Encrypt(entity);

                context.PartnerContacts.Add(entity);

                await context.SaveChangesAsync();

                return PartnerContactErrorCodes.None;
            }
        }

        public async Task<PartnerContactErrorCodes> UpdateAsync(string locationId, string firstName, string lastName, string phoneNumber, string email)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.PartnerContacts.FirstOrDefaultAsync(o => o.LocationId == locationId);

                if (entity == null)
                    return PartnerContactErrorCodes.PartnerContactDoesNotExist;

                entity = _encryptionService.Decrypt(entity);

                entity.FirstName = firstName;
                entity.LastName = lastName;
                entity.PhoneNumber = phoneNumber;
                entity.Email = email;

                entity = _encryptionService.Encrypt(entity);

                context.PartnerContacts.Update(entity);

                await context.SaveChangesAsync();

                return PartnerContactErrorCodes.None;
            }
        }

        private readonly Expression<Func<PartnerContactEntity, PartnerContactModel>> _selectExpression =
            entity => new PartnerContactModel
            {
                LocationId = entity.LocationId,
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                PhoneNumber = entity.PhoneNumber
            };
    }
}
