using System.Collections.Generic;
using System.Threading.Tasks;
using MAVN.Service.CustomerProfile.Domain.Enums;
using MAVN.Service.CustomerProfile.Domain.Models;

namespace MAVN.Service.CustomerProfile.Domain.Repositories
{
    public interface IPartnerContactRepository
    {
        Task<IPartnerContact> GetByLocationIdAsync(string locationId);
        Task<IPartnerContact> GetByEmailAsync(string email);
        Task<IPartnerContact> GetByPhoneAsync(string phone);
        Task DeleteIfExistsAsync(string locationId);
        Task<IEnumerable<IPartnerContact>> GetPaginatedAsync(int skip, int take);
        Task<int> GetTotalAsync();
        Task CreateOrUpdateAsync(PartnerContactModel partnerContact);
    }
}
