using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MAVN.Common.Encryption;
using Lykke.Common.MsSql;
using MAVN.Service.CustomerProfile.Domain.Enums;
using MAVN.Service.CustomerProfile.Domain.Models;
using MAVN.Service.CustomerProfile.Domain.Repositories;
using MAVN.Service.CustomerProfile.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.CustomerProfile.MsSqlRepositories.Repositories
{
    public class ReferralLeadProfileRepository : IReferralLeadProfileRepository
    {
        private readonly MsSqlContextFactory<CustomerProfileContext> _contextFactory;
        private readonly IEncryptionService _encryptionService;

        public ReferralLeadProfileRepository(
            MsSqlContextFactory<CustomerProfileContext> contextFactory,
            IEncryptionService encryptionService)
        {
            _contextFactory = contextFactory;
            _encryptionService = encryptionService;
        }

        public async Task<IReadOnlyList<ReferralLeadProfile>> GetAllAsync()
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entities = await context.ReferralLeadProfiles.ToListAsync();

                return entities
                    .Select(o => ToDomain(_encryptionService.Decrypt(o)))
                    .ToList();
            }
        }

        public async Task<ReferralLeadProfile> GetByIdAsync(Guid referralLeadId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.ReferralLeadProfiles.FindAsync(referralLeadId);

                return entity != null ? ToDomain(_encryptionService.Decrypt(entity)) : null;
            }
        }

        public async Task<ReferralLeadProfileErrorCodes> InsertAsync(ReferralLeadProfile referralLeadProfile)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.ReferralLeadProfiles.FindAsync(referralLeadProfile.ReferralLeadId);

                if (entity != null)
                    return ReferralLeadProfileErrorCodes.ReferralLeadProfileAlreadyExists;

                entity = new ReferralLeadProfileEntity(referralLeadProfile);

                entity = _encryptionService.Encrypt(entity);

                context.ReferralLeadProfiles.Add(entity);

                await context.SaveChangesAsync();
            }

            return ReferralLeadProfileErrorCodes.None;
        }

        public async Task<ReferralLeadProfileErrorCodes> UpdateAsync(ReferralLeadProfile referralLeadProfile)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.ReferralLeadProfiles.FindAsync(referralLeadProfile.ReferralLeadId);

                if (entity == null)
                    return ReferralLeadProfileErrorCodes.ReferralLeadProfileDoesNotExist;

                _encryptionService.Decrypt(entity);

                entity.Update(referralLeadProfile);

                _encryptionService.Encrypt(entity);

                await context.SaveChangesAsync();
            }

            return ReferralLeadProfileErrorCodes.None;
        }

        public async Task DeleteAsync(Guid referralLeadId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.ReferralLeadProfiles.FindAsync(referralLeadId);

                if (entity == null)
                    return;

                using (var transaction = context.Database.BeginTransaction())
                {
                    var archiveEntity = new ReferralLeadProfileArchiveEntity(entity);

                    context.ReferralLeadProfilesArchive.Add(archiveEntity);

                    context.ReferralLeadProfiles.Remove(entity);

                    await context.SaveChangesAsync();

                    transaction.Commit();
                }
            }
        }

        private static ReferralLeadProfile ToDomain(ReferralLeadProfileEntity entity)
            => new ReferralLeadProfile
            {
                ReferralLeadId = entity.ReferralLeadId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                PhoneNumber = entity.PhoneNumber,
                Email = entity.Email,
                Note = entity.Note
            };
    }
}
