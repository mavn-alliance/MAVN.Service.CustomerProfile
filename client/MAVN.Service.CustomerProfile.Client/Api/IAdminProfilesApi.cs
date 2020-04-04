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
    /// Provides methods for work with admin profiles.
    /// </summary>
    [PublicAPI]
    public interface IAdminProfilesApi
    {
        /// <summary>
        /// Returns admin profiles.
        /// </summary>
        /// <param name="identifiers">The list of identifiers to be filtered.</param>
        /// <returns>A collection of admin profiles.</returns>
        [Get("/api/adminProfiles")]
        Task<IReadOnlyList<AdminProfile>> GetAsync([Query(CollectionFormat.Multi)] Guid[] identifiers = null);

        /// <summary>
        /// Returns admin profile by identifier.
        /// </summary>
        /// <param name="adminId">The admin identifier.</param>
        /// <returns>The admin profile response.</returns>
        [Get("/api/adminProfiles/{adminId}")]
        Task<AdminProfileResponse> GetByIdAsync(Guid adminId);

        /// <summary>
        /// Creates new admin profile.
        /// </summary>
        /// <param name="request">The model that represents the admin profile creation information.</param>
        /// <returns>The admin profile response.</returns>
        [Post("/api/adminProfiles")]
        Task<AdminProfileResponse> AddAsync([Body] AdminProfileRequest request);

        /// <summary>
        /// Updates admin profile.
        /// </summary>
        /// <param name="request">The model that represents the admin profile update information.</param>
        /// <returns>The admin profile response.</returns>
        [Put("/api/adminProfiles")]
        Task<AdminProfileResponse> UpdateAsync([Body] AdminProfileRequest request);

        /// <summary>
        /// Deletes admin profile by identifier.
        /// </summary>
        /// <param name="adminId">The admin identifier.</param>
        [Delete("/api/adminProfiles/{adminId}")]
        Task DeleteAsync(Guid adminId);
    }
}
