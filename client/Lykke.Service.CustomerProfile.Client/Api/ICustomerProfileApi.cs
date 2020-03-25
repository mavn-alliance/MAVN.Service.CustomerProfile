using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Service.CustomerProfile.Client.Models;
using Lykke.Service.CustomerProfile.Client.Models.Enums;
using Lykke.Service.CustomerProfile.Client.Models.Requests;
using Lykke.Service.CustomerProfile.Client.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace Lykke.Service.CustomerProfile.Client.Api
{
    /// <summary>
    /// CustomerProfile client API interface.
    /// </summary>
    [PublicAPI]
    public interface ICustomerProfileApi
    {
        /// <summary>
        /// Gets profile information for specific Customer
        /// </summary>
        /// <param name="customerId">The Customer Id</param>
        /// <param name="includeNotVerified">Whether include not verified customers or not</param>
        /// <param name="includeNotActive">Whether to include not active customers or not</param>
        /// <returns>
        /// Customer Profile Response which holds the Profile Information about the Customer
        /// </returns>
        [Get("/api/customers/{customerId}")]
        Task<CustomerProfileResponse> GetByCustomerIdAsync(string customerId, bool includeNotVerified = false, bool includeNotActive = false);

        /// <summary>
        /// Gets paginated list of Customer Profiles
        /// </summary>
        /// <param name="pagingInfo">Information of which page you want the data for</param>
        /// <param name="includeNotVerified">Whether include not verified customers or not</param>
        /// <param name="includeNotActive">Whether to include not active customers or not</param>
        /// <returns>
        /// Paginated Customer Profile Response which holds the Profiles of the Customers in the current page
        /// and Information about the Pagination it self (Current page and page size)
        /// </returns>
        [Get("/api/customers/paginated")]
        Task<PaginatedCustomerProfilesResponse> GetCustomersPaginatedAsync(PaginationModel pagingInfo, bool includeNotVerified = false, bool includeNotActive = false);

        /// <summary>
        /// Searches for the customer profile with a given email.
        /// </summary>
        /// <param name="model">Get by email request model.</param>
        /// <returns>
        /// Customer Profile Response which holds the Profile Information about the Customer
        /// </returns>
        [Post("/api/customers/getbyemail")]
        Task<CustomerProfileResponse> GetByEmailAsync(GetByEmailRequestModel model);

        /// <summary>
        /// Searches for the customer profile with a given phone.
        /// </summary>
        /// <param name="model">Get by phone request model.</param>
        /// <returns></returns>
        [Post("/api/customers/getbyphone")]
        Task<CustomerProfileResponse> GetByPhoneAsync(GetByPhoneRequestModel model);

        /// <summary>
        /// Get collection of profiles by given collection of customer ids
        /// </summary>
        /// <param name="ids">Collection of ids</param>
        /// <param name="includeNotVerified"></param>
        /// <param name="includeNotActive">Whether to include not active customers or not</param>
        /// <returns></returns>
        [Post("/api/customers/list")]
        Task<IEnumerable<Models.Responses.CustomerProfile>> GetByIdsAsync([Body] string[] ids, [Query] bool includeNotVerified, [Query] bool includeNotActive);

        /// <summary>
        /// Creates a Customer Profile if this customer doesn't have a profile already
        /// </summary>
        /// <param name="customerProfile">The data with which the Customer profile needs to be created</param>
        /// <returns></returns>
        [Post("/api/customers")]
        Task<CustomerProfileErrorCodes> CreateIfNotExistAsync([Body] CustomerProfileRequestModel customerProfile);

        /// <summary>
        /// Updates customer profile.
        /// </summary>
        /// <param name="model">The customer profile data.</param>
        [Put("/api/customers")]
        Task<CustomerProfileErrorCodes> UpdateAsync([Body] CustomerProfileUpdateRequestModel model);

        /// <summary>
        /// Updates customer email.
        /// </summary>
        /// <param name="model">The customer profile data.</param>
        [Put("/api/customers/email")]
        Task<CustomerProfileErrorCodes> UpdateEmailAsync([Body] EmailUpdateRequestModel model);

        /// <summary>
        /// Updates customer email.
        /// </summary>
        /// <param name="customerId">The customer id.</param>
        /// <remarks>
        /// Updates customer email.
        ///
        /// Error codes:
        /// - **CustomerProfileDoesNotExist**
        /// - **CustomerIsNotActive**
        /// </remarks>
        /// <returns>
        /// 200 - customer profile successfully updated.
        /// 400 - if an invalid input data was provided
        /// </returns>
        [Put("/api/customers/{customerId}/deactivate")]
        Task<CustomerProfileErrorCodes> RequestDeactivationAsync(string customerId);

        /// <summary>
        /// Deletes/Archives a specific Customer Profile
        /// </summary>
        /// <param name="customerId">The Customer Id</param>
        [Delete("/api/customers/{customerId}")]
        Task DeleteAsync(string customerId);
    }
}
