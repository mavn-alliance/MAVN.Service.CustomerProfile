using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MAVN.Service.CustomerProfile.Domain.Enums;
using MAVN.Service.CustomerProfile.Domain.Models;
using MAVN.Service.CustomerProfile.Domain.Repositories;
using MAVN.Service.CustomerProfile.Domain.Services;

namespace MAVN.Service.CustomerProfile.DomainServices
{
    public class PaymentProviderDetailsService : IPaymentProviderDetailsService
    {
        private readonly IPaymentProviderDetailsRepository _paymentProviderDetailsRepository;

        public PaymentProviderDetailsService(IPaymentProviderDetailsRepository paymentProviderDetailsRepository)
        {
            _paymentProviderDetailsRepository = paymentProviderDetailsRepository;
        }

        public async Task<PaymentProviderDetailsErrorCodes> CreateAsync(IPaymentProviderDetails model)
        {
            var existingPaymentProviderDetails =
               await  _paymentProviderDetailsRepository.GetByPartnerIdAndProviderAsync(model.PartnerId,
                    model.PaymentIntegrationProvider);

            if (existingPaymentProviderDetails != null)
                return PaymentProviderDetailsErrorCodes.PaymentProviderDetailsAlreadyExists;

            await _paymentProviderDetailsRepository.CreateAsync(model);

            return PaymentProviderDetailsErrorCodes.None;
        }

        public async Task<PaymentProviderDetailsErrorCodes> UpdateAsync(IPaymentProviderDetails model)
        {
            var existingPaymentProviderDetails =
                await _paymentProviderDetailsRepository.GetByPartnerIdAndProviderAsync(model.PartnerId,
                    model.PaymentIntegrationProvider);

            if ( existingPaymentProviderDetails != null && existingPaymentProviderDetails.Id != model.Id)
                return PaymentProviderDetailsErrorCodes.PaymentProviderDetailsAlreadyExists;

            var isUpdated = await _paymentProviderDetailsRepository.UpdateAsync(model);

            return isUpdated
                ? PaymentProviderDetailsErrorCodes.None
                : PaymentProviderDetailsErrorCodes.PaymentProviderDetailsDoesNotExist;
        }

        public async Task<PaymentProviderDetailsErrorCodes> DeleteAsync(Guid id)
        {
            var isDeleted = await _paymentProviderDetailsRepository.DeleteAsync(id);

            return isDeleted
                ? PaymentProviderDetailsErrorCodes.None
                : PaymentProviderDetailsErrorCodes.PaymentProviderDetailsDoesNotExist;
        }

        public Task<IReadOnlyList<IPaymentProviderDetails>> GetListByPartnerIdAsync(Guid partnerId)
        {
            var result = _paymentProviderDetailsRepository.GetListByPartnerIdAsync(partnerId);
            return result;
        }

        public Task<IPaymentProviderDetails> GetByPartnerIdAndPaymentProviderAsync(Guid partnerId,
            string paymentProvider)
        {
            var result = _paymentProviderDetailsRepository.GetByPartnerIdAndProviderAsync(partnerId, paymentProvider);
            return result;
        }
    }
}
