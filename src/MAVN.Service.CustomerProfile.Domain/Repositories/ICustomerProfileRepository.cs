using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MAVN.Persistence.PostgreSQL.Legacy;
using MAVN.Service.CustomerProfile.Domain.Enums;
using MAVN.Service.CustomerProfile.Domain.Models;

namespace MAVN.Service.CustomerProfile.Domain.Repositories
{
    public interface ICustomerProfileRepository
    {
        Task<CustomerProfileErrorCodes> CreateIfNotExistAsync(ICustomerProfile customerProfile);

        Task<CustomerProfileErrorCodes> UpdateAsync(
            string customerId,
            string firstName,
            string lastName,
            string phoneNumber,
            string shortPhoneNumber,
            int countryPhoneCodeId,
            int countryOfResidenceId);

        Task<CustomerProfileErrorCodes> UpdateEmailAsync(string customerId, string email, bool isEmailVerified);

        Task<CustomerProfileErrorCodes> UpdateTierAsync(string customerId, string tierId);

        Task<CustomerProfileErrorCodes> UpdatePhoneInfoAsync
            (string customerId, string phoneNumber, string shortPhoneNumber, int countryPhoneCodeId, bool isPhoneVerified);

        Task<(CustomerProfileErrorCodes error, bool wasVerfiedBefore)> SetPhoneAsVerifiedAsync(string customerId);
        Task<IEnumerable<ICustomerProfile>> GetPaginatedAsync(int skip, int take, bool includeNotVerified = false, bool includeNotActive = false);
        Task<ICustomerProfile> GetByCustomerIdAsync(string customerId, bool includeNotVerified = false, bool includeNotActive = false, TransactionContext txContext = null);
        Task<bool> DeleteAsync(string customerId);
        Task<ICustomerProfile> GetByCustomerEmailAsync(string email, bool includeNotVerified = false, bool includeNotActive = false);
        Task<ICustomerProfile> GetByCustomerPhoneAsync(string phone, bool includeNotVerified = false, bool includeNotActive = false);
        Task<(CustomerProfileErrorCodes error, bool wasVerfiedBefore)> SetEmailVerifiedAsync(string customerId);
        Task<IEnumerable<ICustomerProfile>> GetByIdsAsync(IEnumerable<string> ids, bool includeNotVerified = false, bool includeNotActive = false);
        Task<int> GetTotalAsync(bool includeNotVerified = false, bool includeNotActive = false);
        Task<int> GetByPeriodAsync(DateTime startDate, DateTime endDate);
        Task<int> GetTotalByDateAsync(DateTime date, bool includeNotVerified = false);
        Task<bool> IsPhoneNumberUsedByAnotherCustomer(string customerId, string fullPhoneNumber);
        Task ChangeProfileStatus(string customerId, CustomerProfileStatus status, TransactionContext txContext = null);
    }
}
