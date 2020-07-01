using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MAVN.Common.Encryption;
using MAVN.Persistence.PostgreSQL.Legacy;
using MAVN.Service.CustomerProfile.Domain.Enums;
using MAVN.Service.CustomerProfile.Domain.Models;
using MAVN.Service.CustomerProfile.Domain.Repositories;
using MAVN.Service.CustomerProfile.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.CustomerProfile.MsSqlRepositories.Repositories
{
    public class AdminProfileRepository : IAdminProfileRepository
    {
        private readonly PostgreSQLContextFactory<CustomerProfileContext> _contextFactory;
        private readonly IMapper _mapper;
        private readonly IEncryptionService _encryptionService;

        public AdminProfileRepository(
            PostgreSQLContextFactory<CustomerProfileContext> contextFactory,
            IMapper mapper,
            IEncryptionService encryptionService)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
            _encryptionService = encryptionService;
        }

        public async Task<IReadOnlyList<AdminProfile>> GetAllAsync()
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entities = await context.AdminProfiles.ToListAsync();

                return entities
                    .Select(entity => _mapper.Map<AdminProfile>(_encryptionService.Decrypt(entity)))
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
                    .Select(entity => _mapper.Map<AdminProfile>(_encryptionService.Decrypt(entity)))
                    .ToList();
            }
        }

        public async Task<AdminProfile> GetByIdAsync(Guid adminId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.AdminProfiles.FindAsync(adminId);

                return entity != null ? _mapper.Map<AdminProfile>(_encryptionService.Decrypt(entity)) : null;
            }
        }

        public async Task<AdminProfileErrorCodes> InsertAsync(AdminProfile adminProfile)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.AdminProfiles.FindAsync(adminProfile.AdminId);

                if (entity != null)
                    return AdminProfileErrorCodes.AdminProfileAlreadyExists;

                entity = _mapper.Map<AdminProfileEntity>(adminProfile);

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

        public async Task<(AdminProfileErrorCodes error, bool wasVerfiedBefore)> SetEmailVerifiedAsync(Guid adminId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.AdminProfiles
                    .IgnoreQueryFilters()
                    .FirstOrDefaultAsync(c => c.AdminId == adminId);

                if (entity == null)
                    return (AdminProfileErrorCodes.AdminProfileDoesNotExist, false);

                var wasEmailPreviouslyVerified = entity.WasEmailEverVerified;

                if (entity.IsEmailVerified)
                    return (AdminProfileErrorCodes.AdminProfileEmailAlreadyVerified, wasEmailPreviouslyVerified);

                entity.IsEmailVerified = true;
                entity.WasEmailEverVerified = true;

                await context.SaveChangesAsync();

                return (AdminProfileErrorCodes.None, wasEmailPreviouslyVerified);
            }
        }
    }
}
