using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MAVN.Service.CustomerProfile.Client.Models.Requests;
using MAVN.Service.CustomerProfile.Client.Models.Responses;
using Refit;

namespace MAVN.Service.CustomerProfile.Client.Api
{
    /// <summary>
    /// Provides methods for work with referral friend profiles.
    /// </summary>
    [PublicAPI]
    public interface IReferralFriendProfilesApi
    {
        /// <summary>
        /// Returns all referral friend profiles.
        /// </summary>
        /// <returns>A collection of referral friend profiles.</returns>
        [Get("/api/referralFriendProfiles")]
        Task<IReadOnlyList<ReferralFriendProfile>> GetAllAsync();

        /// <summary>
        /// Returns referral friend profile by identifier.
        /// </summary>
        /// <returns>The referral friend profile response.</returns>
        [Get("/api/referralFriendProfiles/{referralFriendId}")]
        Task<ReferralFriendProfileResponse> GetByIdAsync(Guid referralFriendId);

        /// <summary>
        /// Returns referral friend profile by email and referrer.
        /// </summary>
        /// <returns>The referral friend profile response.</returns>
        [Post("/api/referralFriendProfiles/byEmailAndReferrer")]
        Task<ReferralFriendProfileResponse> GetByEmailAndReferrerAsync(
            ReferralFriendByEmailAndReferrerProfileRequest request);

        /// <summary>
        /// Creates new referral friend profile.
        /// </summary>
        /// <param name="request">The model that represents the referral friend profile creation information.</param>
        /// <returns>The referral friend profile response.</returns>
        [Post("/api/referralFriendProfiles")]
        Task<ReferralFriendProfileResponse> AddAsync([Body] ReferralFriendProfileRequest request);

        /// <summary>
        /// Updates referral friend profile.
        /// </summary>
        /// <param name="request">The model that represents the referral friend profile update information.</param>
        /// <returns>The referral friend profile response.</returns>
        [Put("/api/referralFriendProfiles")]
        Task<ReferralFriendProfileResponse> UpdateAsync([Body] ReferralFriendProfileRequest request);

        /// <summary>
        /// Deletes referral friend profile by identifier.
        /// </summary>
        [Delete("/api/referralFriendProfiles/{referralFriendId}")]
        Task DeleteAsync(Guid referralFriendId);
    }
}
