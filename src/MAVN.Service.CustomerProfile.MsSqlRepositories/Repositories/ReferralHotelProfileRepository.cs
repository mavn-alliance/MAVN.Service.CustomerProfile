using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Falcon.Common.Encryption;
using Lykke.Common.MsSql;
using MAVN.Service.CustomerProfile.Domain.Enums;
using MAVN.Service.CustomerProfile.Domain.Models;
using MAVN.Service.CustomerProfile.Domain.Repositories;
using MAVN.Service.CustomerProfile.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.CustomerProfile.MsSqlRepositories.Repositories
{
    public class ReferralHotelProfileRepository : IReferralHotelProfileRepository
    {
        private readonly MsSqlContextFactory<CustomerProfileContext> _contextFactory;
        private readonly IEncryptionService _encryptionService;

        public ReferralHotelProfileRepository(
            MsSqlContextFactory<CustomerProfileContext> contextFactory,
            IEncryptionService encryptionService)
        {
            _contextFactory = contextFactory;
            _encryptionService = encryptionService;
        }

        public async Task<IReadOnlyList<ReferralHotelProfile>> GetAllAsync()
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entities = await context.ReferralHotelProfiles.ToListAsync();

                return entities
                    .Select(o => ToDomain(_encryptionService.Decrypt(o)))
                    .ToList();
            }
        }

        public async Task<ReferralHotelProfile> GetByIdAsync(Guid referralHotelId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.ReferralHotelProfiles.FindAsync(referralHotelId);

                return entity != null ? ToDomain(_encryptionService.Decrypt(entity)) : null;
            }
        }

        public async Task<ReferralHotelProfileErrorCodes> InsertAsync(ReferralHotelProfile referralHotelProfile)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.ReferralHotelProfiles.FindAsync(referralHotelProfile.ReferralHotelId);

                if (entity != null)
                    return ReferralHotelProfileErrorCodes.ReferralHotelProfileAlreadyExists;

                entity = new ReferralHotelProfileEntity(referralHotelProfile);

                entity = _encryptionService.Encrypt(entity);

                context.ReferralHotelProfiles.Add(entity);

                await context.SaveChangesAsync();
            }
            
            return ReferralHotelProfileErrorCodes.None;
        }

        public async Task<ReferralHotelProfileErrorCodes> UpdateAsync(ReferralHotelProfile referralHotelProfile)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.ReferralHotelProfiles.FindAsync(referralHotelProfile.ReferralHotelId);

                if (entity == null)
                    return ReferralHotelProfileErrorCodes.ReferralHotelProfileDoesNotExist;

                _encryptionService.Decrypt(entity);

                entity.Update(referralHotelProfile);

                _encryptionService.Encrypt(entity);

                await context.SaveChangesAsync();
            }

            return ReferralHotelProfileErrorCodes.None;
        }

        public async Task DeleteAsync(Guid referralHotelId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.ReferralHotelProfiles.FindAsync(referralHotelId);

                if (entity == null)
                    return;

                using (var transaction = context.Database.BeginTransaction())
                {
                    var archiveEntity = new ReferralHotelProfileArchiveEntity(entity);

                    context.ReferralHotelProfilesArchive.Add(archiveEntity);

                    context.ReferralHotelProfiles.Remove(entity);

                    await context.SaveChangesAsync();

                    transaction.Commit();
                }
            }
        }

        private static ReferralHotelProfile ToDomain(ReferralHotelProfileEntity entity)
            => new ReferralHotelProfile
            {
                ReferralHotelId = entity.ReferralHotelId,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                Name = entity.Name
            };
    }
}
