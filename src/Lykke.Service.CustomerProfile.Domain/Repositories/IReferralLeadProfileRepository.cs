using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.CustomerProfile.Domain.Enums;
using Lykke.Service.CustomerProfile.Domain.Models;

namespace Lykke.Service.CustomerProfile.Domain.Repositories
{
    public interface IReferralLeadProfileRepository
    {
        Task<IReadOnlyList<ReferralLeadProfile>> GetAllAsync();

        Task<ReferralLeadProfile> GetByIdAsync(Guid referralLeadId);

        Task<ReferralLeadProfileErrorCodes> InsertAsync(ReferralLeadProfile referralLeadProfile);

        Task<ReferralLeadProfileErrorCodes> UpdateAsync(ReferralLeadProfile referralLeadProfile);

        Task DeleteAsync(Guid referralLeadId);
    }
}
