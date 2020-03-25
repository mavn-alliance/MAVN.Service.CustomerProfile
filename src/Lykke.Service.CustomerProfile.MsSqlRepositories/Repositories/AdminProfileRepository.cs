using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Falcon.Common.Encryption;
using Lykke.Common.MsSql;
using Lykke.Service.CustomerProfile.Domain.Enums;
using Lykke.Service.CustomerProfile.Domain.Models;
using Lykke.Service.CustomerProfile.Domain.Repositories;
using Lykke.Service.CustomerProfile.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lykke.Service.CustomerProfile.MsSqlRepositories.Repositories
{
    public class AdminProfileRepository : IAdminProfileRepository
    {
        private readonly MsSqlContextFactory<CustomerProfileContext> _contextFactory;
        private readonly IEncryptionService _encryptionService;

        public AdminProfileRepository(
            MsSqlContextFactory<CustomerProfileContext> contextFactory,
            IEncryptionService encryptionService)
        {
            _contextFactory = contextFactory;
            _encryptionService = encryptionService;
        }

        public async Task<IReadOnlyList<AdminProfile>> GetAllAsync()
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entities = await context.AdminProfiles.ToListAsync();

                return entities
                    .Select(entity => ToDomain(_encryptionService.Decrypt(entity)))
                    .ToList();
            }
        }

        public async Task<IReadOnlyList<AdminProfile>> GetAsync(IReadOnlyList<Guid> identifiers)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entities = await context.AdminProfiles
                    .Where(entity => identifiers.Contains(entity.AdminId))
                    .ToListAsync();

                return entities
                    .Select(entity => ToDomain(_encryptionService.Decrypt(entity)))
                    .ToList();
            }
        }

        public async Task<AdminProfile> GetByIdAsync(Guid adminId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.AdminProfiles.FindAsync(adminId);

                return entity != null ? ToDomain(_encryptionService.Decrypt(entity)) : null;
            }
        }

        public async Task<AdminProfileErrorCodes> InsertAsync(AdminProfile adminProfile)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.AdminProfiles.FindAsync(adminProfile.AdminId);

                if (entity != null)
                    return AdminProfileErrorCodes.AdminProfileAlreadyExists;

                entity = new AdminProfileEntity(adminProfile);

                entity = _encryptionService.Encrypt(entity);

                context.AdminProfiles.Add(entity);

                await context.SaveChangesAsync();
            }

            return AdminProfileErrorCodes.None;
        }

        public async Task<AdminProfileErrorCodes> UpdateAsync(AdminProfile adminProfile)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.AdminProfiles.FindAsync(adminProfile.AdminId);

                if (entity == null)
                    return AdminProfileErrorCodes.AdminProfileDoesNotExist;

                _encryptionService.Decrypt(entity);

                entity.Update(adminProfile);

                _encryptionService.Encrypt(entity);

                await context.SaveChangesAsync();
            }

            return AdminProfileErrorCodes.None;
        }

        public async Task DeleteAsync(Guid adminId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.AdminProfiles.FindAsync(adminId);

                if (entity == null)
                    return;

                using (var transaction = context.Database.BeginTransaction())
                {
                    var archiveEntity = new AdminProfileArchiveEntity(entity);

                    context.AdminProfilesArchive.Add(archiveEntity);

                    context.AdminProfiles.Remove(entity);

                    await context.SaveChangesAsync();

                    transaction.Commit();
                }
            }
        }

        private static AdminProfile ToDomain(AdminProfileEntity entity)
            => new AdminProfile
            {
                AdminId = entity.AdminId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                Company = entity.Company,
                Department = entity.Department,
                JobTitle = entity.JobTitle,
            };
    }
}
