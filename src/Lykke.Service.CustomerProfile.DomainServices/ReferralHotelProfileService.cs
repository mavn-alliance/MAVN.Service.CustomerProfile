using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.Service.CustomerProfile.Domain.Enums;
using Lykke.Service.CustomerProfile.Domain.Models;
using Lykke.Service.CustomerProfile.Domain.Repositories;
using Lykke.Service.CustomerProfile.Domain.Services;

namespace Lykke.Service.CustomerProfile.DomainServices
{
    public class ReferralHotelProfileService : IReferralHotelProfileService
    {
        private readonly IReferralHotelProfileRepository _referralHotelProfileRepository;
        private readonly ILog _log;

        public ReferralHotelProfileService(
            IReferralHotelProfileRepository referralHotelProfileRepository,
            ILogFactory logFactory)
        {
            _referralHotelProfileRepository = referralHotelProfileRepository;
            _log = logFactory.CreateLog(this);
        }

        public Task<IReadOnlyList<ReferralHotelProfile>> GetAllAsync()
        {
            return _referralHotelProfileRepository.GetAllAsync();
        }

        public Task<ReferralHotelProfile> GetByIdAsync(Guid referralHotelId)
        {
            return _referralHotelProfileRepository.GetByIdAsync(referralHotelId);
        }

        public async Task<ReferralHotelProfileErrorCodes> AddAsync(ReferralHotelProfile referralHotelProfile)
        {
            var result = await _referralHotelProfileRepository.InsertAsync(referralHotelProfile);

            if (result == ReferralHotelProfileErrorCodes.None)
            {
                _log.Info("Referral hotel profile created",
                    context: $"referralHotelId: {referralHotelProfile.ReferralHotelId}");
            }
            else
            {
                _log.Info("An error occurred while creating referral hotel profile",
                    context: $"referralHotelId: {referralHotelProfile.ReferralHotelId}; error: {result}");
            }

            return result;
        }

        public async Task<ReferralHotelProfileErrorCodes> UpdateAsync(ReferralHotelProfile referralHotelProfile)
        {
            var result = await _referralHotelProfileRepository.UpdateAsync(referralHotelProfile);

            if (result == ReferralHotelProfileErrorCodes.None)
            {
                _log.Info("Referral hotel profile updated",
                    context: $"referralHotelId: {referralHotelProfile.ReferralHotelId}");
            }
            else
            {
                _log.Info("An error occurred while updating referral hotel profile",
                    context: $"referralHotelId: {referralHotelProfile.ReferralHotelId}; error: {result}");
            }

            return result;
        }

        public async Task DeleteAsync(Guid referralHotelId)
        {
            await _referralHotelProfileRepository.DeleteAsync(referralHotelId);

            _log.Info("Referral hotel profile deleted", context: $"referralHotelId: {referralHotelId}");
        }
    }
}
