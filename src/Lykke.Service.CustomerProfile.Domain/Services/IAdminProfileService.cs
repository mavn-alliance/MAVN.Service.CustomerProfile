using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.CustomerProfile.Domain.Enums;
using Lykke.Service.CustomerProfile.Domain.Models;

namespace Lykke.Service.CustomerProfile.Domain.Services
{
    public interface IAdminProfileService
    {
        Task<IReadOnlyList<AdminProfile>> GetAllAsync();

        Task<IReadOnlyList<AdminProfile>> GetAsync(IReadOnlyList<Guid> identifiers);

        Task<AdminProfile> GetByIdAsync(Guid adminId);

        Task<AdminProfileErrorCodes> AddAsync(AdminProfile adminProfile);

        Task<AdminProfileErrorCodes> UpdateAsync(AdminProfile adminProfile);

        Task DeleteAsync(Guid adminId);
    }
}
