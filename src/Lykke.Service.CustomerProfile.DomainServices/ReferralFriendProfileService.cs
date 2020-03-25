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
    public class ReferralFriendProfileService : IReferralFriendProfileService
    {
        private readonly IReferralFriendProfileRepository _referralFriendProfileRepository;
        private readonly ILog _log;

        public ReferralFriendProfileService(
            IReferralFriendProfileRepository referralFriendProfileRepository,
            ILogFactory logFactory)
        {
            _referralFriendProfileRepository = referralFriendProfileRepository;
            _log = logFactory.CreateLog(this);
        }

        public async Task<IReadOnlyList<ReferralFriendProfile>> GetAllAsync()
        {
            return await _referralFriendProfileRepository.GetAllAsync();
        }

        public async Task<ReferralFriendProfile> GetByIdAsync(Guid referralFriendId)
        {
            return await _referralFriendProfileRepository.GetByIdAsync(referralFriendId);
        }

        public async Task<ReferralFriendProfile> GetByEmailAndReferrerAsync(string email, Guid referrerId)
        {
            return await _referralFriendProfileRepository.GetByEmailAndReferrerAsync(email, referrerId);
        }

        public async Task<ReferralFriendProfileErrorCodes> AddAsync(ReferralFriendProfile referralFriendProfile)
        {
            var result = await _referralFriendProfileRepository.InsertAsync(referralFriendProfile);

            if (result == ReferralFriendProfileErrorCodes.None)
            {
                _log.Info("Referral friend profile created",
                    context: $"referralFriendId: {referralFriendProfile.ReferralFriendId}");
            }
            else
            {
                _log.Info("An error occurred while creating referral friend profile",
                    context: $"referralHotelId: {referralFriendProfile.ReferralFriendId}; error: {result}");
            }

            return result;
        }

        public async Task<ReferralFriendProfileErrorCodes> UpdateAsync(ReferralFriendProfile referralFriendProfile)
        {
            var result = await _referralFriendProfileRepository.UpdateAsync(referralFriendProfile);

            if (result == ReferralFriendProfileErrorCodes.None)
            {
                _log.Info("Referral friend profile updated",
                    context: $"referralFriendId: {referralFriendProfile.ReferralFriendId}");
            }
            else
            {
                _log.Info("An error occurred while updating referral friend profile",
                    context: $"referralHotelId: {referralFriendProfile.ReferralFriendId}; error: {result}");
            }

            return result;
        }

        public async Task DeleteAsync(Guid referralFriendId)
        {
            await _referralFriendProfileRepository.DeleteAsync(referralFriendId);

            _log.Info("Referral friend profile deleted", context: $"referralFriendId: {referralFriendId}");
        }
    }
}
