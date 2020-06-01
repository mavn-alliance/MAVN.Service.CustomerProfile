using System.Threading.Tasks;
using MAVN.Service.CustomerProfile.Domain.Enums;
using MAVN.Service.CustomerProfile.Domain.Models;

namespace MAVN.Service.CustomerProfile.Domain.Services
{
    public interface IPartnerContactService
    {
        Task<PartnerContactResult> GetByLocationIdAsync(string locationId);
        Task<PaginatedPartnerContactsModel> GetPaginatedAsync(int currentPage, int pageSize);
        Task CreateOrUpdateAsync(PartnerContactModel partnerContact);
        Task RemoveIfExistsAsync(string locationId);
    }
}
