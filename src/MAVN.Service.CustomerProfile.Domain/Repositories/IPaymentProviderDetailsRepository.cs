using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MAVN.Service.CustomerProfile.Domain.Models;

namespace MAVN.Service.CustomerProfile.Domain.Repositories
{
    public interface IPaymentProviderDetailsRepository
    {
        Task CreateAsync(IPaymentProviderDetails model);
        Task<bool> UpdateAsync(IPaymentProviderDetails model);
        Task<bool> DeleteAsync(Guid id);
        Task<IReadOnlyList<IPaymentProviderDetails>> GetListByPartnerIdAsync(Guid partnerId);
        Task<IPaymentProviderDetails> GetByPartnerIdAndProviderAsync(Guid partnerId, string paymentProvider);
    }
}
