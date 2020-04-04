using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MAVN.Service.CustomerProfile.Domain.Enums;
using MAVN.Service.CustomerProfile.Domain.Models;

namespace MAVN.Service.CustomerProfile.Domain.Repositories
{
    public interface IAdminProfileRepository
    {
        Task<IReadOnlyList<AdminProfile>> GetAllAsync();

        Task<IReadOnlyList<AdminProfile>> GetAsync(IReadOnlyList<Guid> identifiers);

        Task<AdminProfile> GetByIdAsync(Guid adminId);

        Task<AdminProfileErrorCodes> InsertAsync(AdminProfile adminProfile);

        Task<AdminProfileErrorCodes> UpdateAsync(AdminProfile adminProfile);

        Task DeleteAsync(Guid adminId);
    }
}
