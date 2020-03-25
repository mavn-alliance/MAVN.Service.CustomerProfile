using Lykke.Common.MsSql;
using Lykke.Service.CustomerProfile.Domain.Enums;
using Lykke.Service.CustomerProfile.Domain.Models;
using Lykke.Service.CustomerProfile.Domain.Repositories;
using Lykke.Service.CustomerProfile.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Falcon.Common.Encryption;

namespace Lykke.Service.CustomerProfile.MsSqlRepositories.Repositories
{
    public class ReferralFriendProfileRepository : IReferralFriendProfileRepository
    {
        private readonly MsSqlContextFactory<CustomerProfileContext> _contextFactory;
        private readonly IEncryptionService _encryptionService;

        public ReferralFriendProfileRepository(
            MsSqlContextFactory<CustomerProfileContext> contextFactory,
            IEncryptionService encryptionService)
        {
            _contextFactory = contextFactory;
            _encryptionService = encryptionService;
        }

        public async Task<IReadOnlyList<ReferralFriendProfile>> GetAllAsync()
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entities = await context.ReferralFriendProfiles.ToListAsync();

                return entities
                    .Select(o => ToDomain(_encryptionService.Decrypt(o)))
                    .ToList();
            }
        }

        public async Task<ReferralFriendProfile> GetByIdAsync(Guid referralFriendId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.ReferralFriendProfiles.FindAsync(referralFriendId);

                return entity != null ? ToDomain(_encryptionService.Decrypt(entity)) : null;
            }
        }

        public async Task<ReferralFriendProfile> GetByEmailAndReferrerAsync(string email, Guid referrerId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var encryptEmail = _encryptionService.EncryptValue(email.ToLower());
                var entity = await context.ReferralFriendProfiles
                    .FirstOrDefaultAsync(r => r.Email == encryptEmail && r.ReferrerId == referrerId);

                return entity != null ? ToDomain(_encryptionService.Decrypt(entity)) : null;
            }
        }

        public async Task<ReferralFriendProfileErrorCodes> InsertAsync(ReferralFriendProfile referralFriendProfile)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.ReferralFriendProfiles.FindAsync(referralFriendProfile.ReferralFriendId);

                if (entity != null)
                    return ReferralFriendProfileErrorCodes.ReferralFriendProfileAlreadyExists;

                entity = new ReferralFriendProfileEntity(referralFriendProfile);

                entity = _encryptionService.Encrypt(entity);

                context.ReferralFriendProfiles.Add(entity);

                await context.SaveChangesAsync();
            }

            return ReferralFriendProfileErrorCodes.None;
        }

        public async Task<ReferralFriendProfileErrorCodes> UpdateAsync(ReferralFriendProfile referralFriendProfile)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.ReferralFriendProfiles.FindAsync(referralFriendProfile.ReferralFriendId);

                if (entity == null)
                    return ReferralFriendProfileErrorCodes.ReferralFriendProfileDoesNotExist;

                _encryptionService.Decrypt(entity);

                entity.Update(referralFriendProfile);

                _encryptionService.Encrypt(entity);

                await context.SaveChangesAsync();
            }

            return ReferralFriendProfileErrorCodes.None;
        }

        public async Task DeleteAsync(Guid referralFriendId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.ReferralFriendProfiles.FindAsync(referralFriendId);

                if (entity == null)
                    return;

                using (var transaction = context.Database.BeginTransaction())
                {
                    var archiveEntity = new ReferralFriendProfileArchiveEntity(entity);

                    context.ReferralFriendProfilesArchive.Add(archiveEntity);

                    context.ReferralFriendProfiles.Remove(entity);

                    await context.SaveChangesAsync();

                    transaction.Commit();
                }
            }
        }

        private static ReferralFriendProfile ToDomain(ReferralFriendProfileEntity entity)
            => new ReferralFriendProfile
            {
                ReferralFriendId = entity.ReferralFriendId,
                ReferrerId = entity.ReferrerId,
                FullName = entity.FullName,
                Email = entity.Email
            };
    }
}
