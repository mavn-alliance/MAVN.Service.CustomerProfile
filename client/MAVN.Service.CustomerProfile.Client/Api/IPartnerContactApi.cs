using System.Threading.Tasks;
using JetBrains.Annotations;
using MAVN.Service.CustomerProfile.Client.Models;
using MAVN.Service.CustomerProfile.Client.Models.Enums;
using MAVN.Service.CustomerProfile.Client.Models.Requests;
using MAVN.Service.CustomerProfile.Client.Models.Responses;
using Refit;

namespace MAVN.Service.CustomerProfile.Client.Api
{
    /// <summary>
    /// PartnerContactApi client API interface.
    /// </summary>
    [PublicAPI]
    public interface IPartnerContactApi
    {
        /// <summary>
        /// Gets profile information for specific Partner contact
        /// </summary>
        /// <param name="locationId">The Partner contact Id</param>
        /// <returns>
        /// Partner contact Response which holds the Profile Information about the Partner contact
        /// </returns>
        [Get("/api/partnerContacts/{locationId}")]
        Task<PartnerContactResponse> GetByLocationIdAsync(string locationId);

        /// <summary>
        /// Gets paginated list of Partner contacts
        /// </summary>
        /// <param name="pagingInfo">Information of which page you want the data for</param>
        /// <returns>
        /// Paginated Partner contact Response which holds the Profiles of the Partner contacts in the current page
        /// and Information about the Pagination it self (Current page and page size)
        /// </returns>
        [Get("/api/partnerContacts/paginated")]
        Task<PaginatedPartnerContactsResponse> GetPartnerContactsPaginatedAsync(PaginationModel pagingInfo);

        /// <summary>
        /// Creates a Partner contact if this Partner contact doesn't have a profile already
        /// </summary>
        /// <param name="partnerContactRequest">The data with which the Partner contact needs to be created</param>
        /// <returns></returns>
        [Post("/api/partnerContacts")]
        Task<PartnerContactErrorCodes> CreateIfNotExistAsync([Body] PartnerContactRequestModel partnerContactRequest);

        /// <summary>
        /// Updates Partner contact.
        /// </summary>
        /// <param name="partnerContactUpdate">The Partner contact data.</param>
        [Put("/api/partnerContacts")]
        Task<PartnerContactErrorCodes> UpdateAsync([Body] PartnerContactUpdateRequestModel partnerContactUpdate);

        /// <summary>
        /// Deletes/Archives a specific Partner contact
        /// </summary>
        /// <param name="locationId">The Location Id</param>
        [Delete("/api/partnerContacts/{locationId}")]
        Task DeleteAsync(string locationId);
    }
}
