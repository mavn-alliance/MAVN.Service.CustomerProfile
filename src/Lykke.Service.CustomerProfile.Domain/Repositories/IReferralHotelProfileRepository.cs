using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.CustomerProfile.Domain.Enums;
using Lykke.Service.CustomerProfile.Domain.Models;

namespace Lykke.Service.CustomerProfile.Domain.Repositories
{
    public interface IReferralHotelProfileRepository
    {
        Task<IReadOnlyList<ReferralHotelProfile>> GetAllAsync();

        Task<ReferralHotelProfile> GetByIdAsync(Guid referralHotelId);

        Task<ReferralHotelProfileErrorCodes> InsertAsync(ReferralHotelProfile referralHotelProfile);

        Task<ReferralHotelProfileErrorCodes> UpdateAsync(ReferralHotelProfile referralHotelProfile);

        Task DeleteAsync(Guid referralHotelId);
    }
}
