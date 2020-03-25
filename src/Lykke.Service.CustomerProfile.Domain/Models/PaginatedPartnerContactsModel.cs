using System.Collections.Generic;

namespace Lykke.Service.CustomerProfile.Domain.Models
{
    public class PaginatedPartnerContactsModel
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<IPartnerContact> PartnerContacts { get; set; }
    }
}
