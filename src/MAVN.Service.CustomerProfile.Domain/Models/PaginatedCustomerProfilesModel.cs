using System.Collections.Generic;

namespace MAVN.Service.CustomerProfile.Domain.Models
{
    public class PaginatedCustomerProfilesModel
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<ICustomerProfile> Customers { get; set; }
    }
}
