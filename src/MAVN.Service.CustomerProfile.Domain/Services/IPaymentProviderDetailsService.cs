using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MAVN.Service.CustomerProfile.Domain.Enums;
using MAVN.Service.CustomerProfile.Domain.Models;

namespace MAVN.Service.CustomerProfile.Domain.Services
{
    public interface IPaymentProviderDetailsService
    {
        Task<PaymentProviderDetailsErrorCodes> CreateAsync(IPaymentProviderDetails model);
        Task<PaymentProviderDetailsErrorCodes> UpdateAsync(IPaymentProviderDetails model);
        Task<PaymentProviderDetailsErrorCodes> DeleteAsync(Guid id);
        Task<IReadOnlyList<IPaymentProviderDetails>> GetListByPartnerIdAsync(Guid partnerId);
        Task<IPaymentProviderDetails> GetByPartnerIdAndPaymentProviderAsync(Guid partnerId, string paymentProvider);
    }
}
