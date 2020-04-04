using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MAVN.Service.CustomerProfile.Domain.Enums;
using MAVN.Service.CustomerProfile.Domain.Models;

namespace MAVN.Service.CustomerProfile.Domain.Services
{
    public interface IReferralLeadProfileService
    {
        Task<IReadOnlyList<ReferralLeadProfile>> GetAllAsync();

        Task<ReferralLeadProfile> GetByIdAsync(Guid referralLeadId);

        Task<ReferralLeadProfileErrorCodes> AddAsync(ReferralLeadProfile referralLeadProfile);

        Task<ReferralLeadProfileErrorCodes> UpdateAsync(ReferralLeadProfile referralLeadProfile);

        Task DeleteAsync(Guid referralLeadId);
    }
}
