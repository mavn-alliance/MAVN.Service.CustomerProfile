using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MAVN.Service.CustomerProfile.Domain.Enums;
using MAVN.Service.CustomerProfile.Domain.Models;

namespace MAVN.Service.CustomerProfile.Domain.Repositories
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
