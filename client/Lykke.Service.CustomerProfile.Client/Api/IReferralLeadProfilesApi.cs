using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Service.CustomerProfile.Client.Models.Requests;
using Lykke.Service.CustomerProfile.Client.Models.Responses;
using Refit;

namespace Lykke.Service.CustomerProfile.Client.Api
{
    /// <summary>
    /// Provides methods for work with referral lead profiles.
    /// </summary>
    [PublicAPI]
    public interface IReferralLeadProfilesApi
    {
        /// <summary>
        /// Returns all referral lead profiles.
        /// </summary>
        /// <returns>A collection of referral lead profiles.</returns>
        [Get("/api/referralLeadProfiles")]
        Task<IReadOnlyList<ReferralLeadProfile>> GetAllAsync();

        /// <summary>
        /// Returns referral lead profile by identifier.
        /// </summary>
        /// <returns>The referral lead profile response.</returns>
        [Get("/api/referralLeadProfiles/{referralLeadId}")]
        Task<ReferralLeadProfileResponse> GetByIdAsync(Guid referralLeadId);

        /// <summary>
        /// Creates new referral lead profile.
        /// </summary>
        /// <param name="request">The model that represents the referral lead profile creation information.</param>
        /// <returns>The referral lead profile response.</returns>
        [Post("/api/referralLeadProfiles")]
        Task<ReferralLeadProfileResponse> AddAsync([Body] ReferralLeadProfileRequest request);
        
        /// <summary>
        /// Updates referral lead profile.
        /// </summary>
        /// <param name="request">The model that represents the referral lead profile update information.</param>
        /// <returns>The referral lead profile response.</returns>
        [Put("/api/referralLeadProfiles")]
        Task<ReferralLeadProfileResponse> UpdateAsync([Body] ReferralLeadProfileRequest request);
        
        /// <summary>
        /// Deletes referral lead profile by identifier.
        /// </summary>
        [Delete("/api/referralLeadProfiles/{referralLeadId}")]
        Task DeleteAsync(Guid referralLeadId);
    }
}
