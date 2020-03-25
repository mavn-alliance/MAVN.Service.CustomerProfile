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
    public class ReferralLeadProfileService : IReferralLeadProfileService
    {
        private readonly IReferralLeadProfileRepository _referralLeadProfileRepository;
        private readonly ILog _log;

        public ReferralLeadProfileService(
            IReferralLeadProfileRepository referralLeadProfileRepository,
            ILogFactory logFactory)
        {
            _referralLeadProfileRepository = referralLeadProfileRepository;
            _log = logFactory.CreateLog(this);
        }

        public Task<IReadOnlyList<ReferralLeadProfile>> GetAllAsync()
        {
            return _referralLeadProfileRepository.GetAllAsync();
        }

        public Task<ReferralLeadProfile> GetByIdAsync(Guid referralLeadId)
        {
            return _referralLeadProfileRepository.GetByIdAsync(referralLeadId);
        }

        public async Task<ReferralLeadProfileErrorCodes> AddAsync(ReferralLeadProfile referralLeadProfile)
        {
            var result = await _referralLeadProfileRepository.InsertAsync(referralLeadProfile);

            if (result == ReferralLeadProfileErrorCodes.None)
            {
                _log.Info("Referral lead profile created",
                    context: $"referralLeadId: {referralLeadProfile.ReferralLeadId}");
            }
            else
            {
                _log.Info("An error occurred while creating referral lead profile",
                    context: $"referralHotelId: {referralLeadProfile.ReferralLeadId}; error: {result}");
            }

            return result;
        }

        public async Task<ReferralLeadProfileErrorCodes> UpdateAsync(ReferralLeadProfile referralLeadProfile)
        {
            var result = await _referralLeadProfileRepository.UpdateAsync(referralLeadProfile);

            if (result == ReferralLeadProfileErrorCodes.None)
            {
                _log.Info("Referral lead profile updated",
                    context: $"referralLeadId: {referralLeadProfile.ReferralLeadId}");
            }
            else
            {
                _log.Info("An error occurred while updating referral lead profile",
                    context: $"referralHotelId: {referralLeadProfile.ReferralLeadId}; error: {result}");
            }

            return result;
        }

        public async Task DeleteAsync(Guid referralLeadId)
        {
            await _referralLeadProfileRepository.DeleteAsync(referralLeadId);

            _log.Info("Referral lead profile deleted", context: $"referralLeadId: {referralLeadId}");
        }
    }
}
