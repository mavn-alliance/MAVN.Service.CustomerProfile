using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.CustomerProfile.Domain.Enums;
using Lykke.Service.CustomerProfile.Domain.Models;

namespace Lykke.Service.CustomerProfile.Domain.Repositories
{
    public interface IPartnerContactRepository
    {
        Task<IPartnerContact> GetByLocationIdAsync(string locationId);
        Task<IPartnerContact> GetByEmailAsync(string email);
        Task<IPartnerContact> GetByPhoneAsync(string phone);
        Task<bool> DeleteAsync(string locationId);
        Task<IEnumerable<IPartnerContact>> GetPaginatedAsync(int skip, int take);
        Task<int> GetTotalAsync();
        Task<PartnerContactErrorCodes> CreateIfNotExistAsync(PartnerContactModel partnerContact);
        Task<PartnerContactErrorCodes> UpdateAsync(string locationId, string firstName, string lastName, string phoneNumber, string email);
    }
}
