using System.Collections.Generic;
using System.Threading.Tasks;
using MAVN.Service.CustomerProfile.Domain.Enums;
using MAVN.Service.CustomerProfile.Domain.Models;

namespace MAVN.Service.CustomerProfile.Domain.Services
{
    public interface ICustomerProfileService
    {
        Task<CustomerProfileResult> GetByCustomerIdAsync(string customerId, bool includeNotVerified = false, bool includeNotActive = false);
        Task<CustomerProfileErrorCodes> CreateIfNotExistsAsync(ICustomerProfile customerProfile);
        Task<PaginatedCustomerProfilesModel> GetPaginatedAsync(int currentPage, int pageSize, bool includeNotVerified = false, bool includeNotActive = false);
        Task RemoveAsync(string customerId);
        Task<CustomerProfileResult> GetByEmailAsync(string email, bool includeNotVerified = false, bool includeNotActive = false);
        Task<CustomerProfileResult> GetByPhoneAsync(string phone, bool includeNotVerified = false, bool includeNotActive = false);
        Task<CustomerProfileErrorCodes> SetEmailAsVerifiedAsync(string customerId);
        Task<IEnumerable<ICustomerProfile>> GetByCustomerIdsAsync(IEnumerable<string> ids, bool includeNotVerified = false, bool includeNotActive = false);
        Task<CustomerProfileErrorCodes> UpdateAsync(
            string customerId,
            string firstName,
            string lastName,
            string phoneNumber,
            int countryPhoneCodeId,
            int countryOfResidenceId);
        Task<CustomerProfileErrorCodes> UpdateEmailAsync(string customerId, string email);
        Task<CustomerProfileErrorCodes> UpdateTierAsync(string customerId, string tierId);
        Task<CustomerProfileErrorCodes> UpdatePhoneInfoAsync(string customerId, string phoneNumber, int countryPhoneCodeId);
        Task<CustomerProfileErrorCodes> SetPhoneAsVerifiedAsync(string customerId);
        Task<CustomerProfileErrorCodes> RequestCustomerProfileDeactivation(string customerId);
        Task MarkCustomerAsDeactivated(string customerId);
    }
}
