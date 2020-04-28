using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using MAVN.Service.CustomerProfile.Client.Api;
using MAVN.Service.CustomerProfile.Client.Models.Enums;
using MAVN.Service.CustomerProfile.Client.Models.Requests;
using MAVN.Service.CustomerProfile.Client.Models.Responses;
using MAVN.Service.CustomerProfile.Domain.Models;
using MAVN.Service.CustomerProfile.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MAVN.Service.CustomerProfile.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/paymentProviderDetails")]
    public class PaymentProviderDetailsController : ControllerBase, IPaymentProviderDetailsApi
    {
        private readonly IPaymentProviderDetailsService _paymentProviderDetailsService;
        private readonly IMapper _mapper;

        public PaymentProviderDetailsController(IPaymentProviderDetailsService paymentProviderDetailsService, IMapper mapper)
        {
            _paymentProviderDetailsService = paymentProviderDetailsService;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates payment provider details
        /// </summary>
        /// <returns><see cref="PaymentProviderDetailsErrorCodes"/></returns>
        [HttpPost]
        [ProducesResponseType(typeof(PaymentProviderDetailsErrorCodes), (int)HttpStatusCode.OK)]
        public async Task<PaymentProviderDetailsErrorCodes> CreateAsync(CreatePaymentProviderDetailsRequest request)
        {
            var model = _mapper.Map<IPaymentProviderDetails>(request);
            var result = await _paymentProviderDetailsService.CreateAsync(model);

            return (PaymentProviderDetailsErrorCodes)result;
        }

        /// <summary>
        /// Updates payment provider details
        /// </summary>
        /// <returns><see cref="PaymentProviderDetailsErrorCodes"/></returns>
        [HttpPut]
        [ProducesResponseType(typeof(PaymentProviderDetailsErrorCodes), (int)HttpStatusCode.OK)]
        public async Task<PaymentProviderDetailsErrorCodes> UpdateAsync(EditPaymentProviderDetailsRequest request)
        {
            var model = _mapper.Map<IPaymentProviderDetails>(request);
            var result = await _paymentProviderDetailsService.UpdateAsync(model);

            return (PaymentProviderDetailsErrorCodes)result;
        }

        /// <summary>
        /// Deletes payment provider details
        /// </summary>
        /// <returns><see cref="PaymentProviderDetailsErrorCodes"/></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(PaymentProviderDetailsErrorCodes), (int)HttpStatusCode.OK)]
        public async Task<PaymentProviderDetailsErrorCodes> DeleteAsync([FromRoute]Guid id)
        {
            var result = await _paymentProviderDetailsService.DeleteAsync(id);

            return (PaymentProviderDetailsErrorCodes)result;
        }

        /// <summary>
        /// Returns all payment provider details for specific partner
        /// </summary>
        /// <returns><see cref="IReadOnlyList<PaymentProviderDetails>"/></returns>
        [HttpGet("{partnerId}")]
        [ProducesResponseType(typeof(IReadOnlyList<PaymentProviderDetails>), (int)HttpStatusCode.OK)]
        public async Task<IReadOnlyList<PaymentProviderDetails>> GetListByPartnerIdAsync([FromRoute]Guid partnerId)
        {
            var result = await _paymentProviderDetailsService.GetListByPartnerIdAsync(partnerId);

            return _mapper.Map<IReadOnlyList<PaymentProviderDetails>>(result);
        }

        /// <summary>
        /// Returns all payment provider details for specific partner
        /// </summary>
        /// <returns><see cref="GetByPartnerIdAndPaymentProviderResponse"/></returns>
        [HttpGet]
        [ProducesResponseType(typeof(GetByPartnerIdAndPaymentProviderResponse), (int)HttpStatusCode.OK)]
        public async Task<GetByPartnerIdAndPaymentProviderResponse> GetByPartnerIdAndPaymentProviderAsync([Required]Guid partnerId, [Required] string paymentProvider)
        {
            var result = await _paymentProviderDetailsService.GetByPartnerIdAndPaymentProviderAsync(partnerId, paymentProvider);

            if (result == null)
                return new GetByPartnerIdAndPaymentProviderResponse
                {
                    ErrorCode = PaymentProviderDetailsErrorCodes.PaymentProviderDetailsDoesNotExist
                };

            return new GetByPartnerIdAndPaymentProviderResponse
            {
                PaymentProviderDetails = _mapper.Map<PaymentProviderDetails>(result),
                ErrorCode = PaymentProviderDetailsErrorCodes.None
            };
        }
    }
}
