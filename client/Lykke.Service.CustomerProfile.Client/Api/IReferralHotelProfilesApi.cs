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
    /// Provides methods for work with referral hotel profiles.
    /// </summary>
    [PublicAPI]
    public interface IReferralHotelProfilesApi
    {
        /// <summary>
        /// Returns all referral hotel profiles.
        /// </summary>
        /// <returns>A collection of referral hotel profiles.</returns>
        [Get("/api/referralHotelProfiles")]
        Task<IReadOnlyList<ReferralHotelProfile>> GetAllAsync();

        /// <summary>
        /// Returns referral hotel profile by identifier.
        /// </summary>
        /// <param name="referralHotelId">The referral hotel identifier.</param>
        /// <returns>The referral hotel profile response.</returns>
        [Get("/api/referralHotelProfiles/{referralHotelId}")]
        Task<ReferralHotelProfileResponse> GetByIdAsync(Guid referralHotelId);

        /// <summary>
        /// Creates new referral hotel profile.
        /// </summary>
        /// <param name="request">The model that represents the referral hotel profile creation information.</param>
        /// <returns>The referral hotel profile response.</returns>
        [Post("/api/referralHotelProfiles")]
        Task<ReferralHotelProfileResponse> AddAsync([Body] ReferralHotelProfileRequest request);

        /// <summary>
        /// Updates referral hotel profile.
        /// </summary>
        /// <param name="request">The model that represents the referral hotel profile update information.</param>
        /// <returns>The referral hotel profile response.</returns>
        [Put("/api/referralHotelProfiles")]
        Task<ReferralHotelProfileResponse> UpdateAsync([Body] ReferralHotelProfileRequest request);

        /// <summary>
        /// Deletes referral hotel profile by identifier.
        /// </summary>
        [Delete("/api/referralHotelProfiles/{referralHotelId}")]
        Task DeleteAsync(Guid referralHotelId);
    }
}
