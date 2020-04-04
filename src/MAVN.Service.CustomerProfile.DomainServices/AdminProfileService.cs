using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using MAVN.Service.CustomerProfile.Domain.Enums;
using MAVN.Service.CustomerProfile.Domain.Models;
using MAVN.Service.CustomerProfile.Domain.Repositories;
using MAVN.Service.CustomerProfile.Domain.Services;

namespace MAVN.Service.CustomerProfile.DomainServices
{
    public class AdminProfileService : IAdminProfileService
    {
        private readonly IAdminProfileRepository _adminProfileRepository;
        private readonly ILog _log;

        public AdminProfileService(
            IAdminProfileRepository adminProfileRepository,
            ILogFactory logFactory)
        {
            _adminProfileRepository = adminProfileRepository;
            _log = logFactory.CreateLog(this);
        }

        public Task<IReadOnlyList<AdminProfile>> GetAllAsync()
        {
            return _adminProfileRepository.GetAllAsync();
        }

        public Task<IReadOnlyList<AdminProfile>> GetAsync(IReadOnlyList<Guid> identifiers)
        {
            return _adminProfileRepository.GetAsync(identifiers);
        }

        public Task<AdminProfile> GetByIdAsync(Guid adminId)
        {
            return _adminProfileRepository.GetByIdAsync(adminId);
        }

        public async Task<AdminProfileErrorCodes> AddAsync(AdminProfile adminProfile)
        {
            var result = await _adminProfileRepository.InsertAsync(adminProfile);

            if (result == AdminProfileErrorCodes.None)
            {
                _log.Info("Admin profile created", context: $"adminId: {adminProfile.AdminId}");
            }
            else
            {
                _log.Info("An error occurred while creating admin profile",
                    context: $"adminId: {adminProfile.AdminId}; error: {result}");
            }

            return result;
        }

        public async Task<AdminProfileErrorCodes> UpdateAsync(AdminProfile adminProfile)
        {
            var result = await _adminProfileRepository.UpdateAsync(adminProfile);

            if (result == AdminProfileErrorCodes.None)
            {
                _log.Info("Admin profile updated", context: $"adminId: {adminProfile.AdminId}");
            }
            else
            {
                _log.Info("An error occurred while updating admin profile",
                    context: $"adminId: {adminProfile.AdminId}; error: {result}");
            }

            return result;
        }

        public async Task DeleteAsync(Guid adminId)
        {
            await _adminProfileRepository.DeleteAsync(adminId);

            _log.Info("Admin profile deleted", context: $"adminId: {adminId}");
        }
    }
}
