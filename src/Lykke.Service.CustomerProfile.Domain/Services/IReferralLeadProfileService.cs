using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.CustomerProfile.Domain.Enums;
using Lykke.Service.CustomerProfile.Domain.Models;

namespace Lykke.Service.CustomerProfile.Domain.Services
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
