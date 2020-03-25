using System.Collections.Generic;
using JetBrains.Annotations;

namespace Lykke.Service.CustomerProfile.Client.Models.Responses
{
    /// <summary>
    /// Gives the List of Customers on the desired Page and information about the Page
    /// </summary>
    [PublicAPI]
    public class PaginatedCustomerProfilesResponse
    {
        /// <summary>
        /// Current page in pagination result
        /// </summary>
        public int CurrentPage { get; set; }
        
        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; }
        
        /// <summary>
        /// Total count of records
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// List of Customers for the given page
        /// </summary>
        public IEnumerable<CustomerProfile> Customers { get; set; }
    }
}
