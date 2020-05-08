using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MAVN.Service.CustomerProfile.Client.Models.Enums;
using MAVN.Service.CustomerProfile.Client.Models.Requests;
using MAVN.Service.CustomerProfile.Client.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace MAVN.Service.CustomerProfile.Client.Api
{
    /// <summary>
    /// PaymentProviderDetails API
    /// </summary>
    public interface IPaymentProviderDetailsApi
    {
        /// <summary>
        /// Creates payment provider details
        /// </summary>
        /// <returns><see cref="PaymentProviderDetailsErrorCodes"/></returns>
        [Post("/api/paymentProviderDetails")]
        Task<PaymentProviderDetailsErrorCodes> CreateAsync([Body] CreatePaymentProviderDetailsRequest request);

        /// <summary>
        /// Updates payment provider details
        /// </summary>
        /// <returns><see cref="PaymentProviderDetailsErrorCodes"/></returns>
        [Put("/api/paymentProviderDetails")]
        [ProducesResponseType(typeof(PaymentProviderDetailsErrorCodes), (int) HttpStatusCode.OK)]
        Task<PaymentProviderDetailsErrorCodes> UpdateAsync([Body] EditPaymentProviderDetailsRequest request);

        /// <summary>
        /// Deletes payment provider details
        /// </summary>
        /// <returns><see cref="PaymentProviderDetailsErrorCodes"/></returns>
        [Delete("/api/paymentProviderDetails/{id}")]
        Task<PaymentProviderDetailsErrorCodes> DeleteAsync(Guid id);

        /// <summary>
        /// Returns all payment provider details for specific partner
        /// </summary>
        /// <returns><see cref="IReadOnlyList{T}<PaymentProviderDetails>"/></returns>
        [Get("/api/paymentProviderDetails/{partnerId}")]
        Task<IReadOnlyList<PaymentProviderDetails>> GetListByPartnerIdAsync([FromRoute] Guid partnerId);

        /// <summary>
        /// Returns all payment provider details for specific partner
        /// </summary>
        /// <returns><see cref="GetByPartnerIdAndPaymentProviderResponse"/></returns>
        [Get("/api/paymentProviderDetails")]
        Task<GetByPartnerIdAndPaymentProviderResponse> GetByPartnerIdAndPaymentProviderAsync([Query]Guid partnerId,
            [Query]string paymentProvider);
    }
}
