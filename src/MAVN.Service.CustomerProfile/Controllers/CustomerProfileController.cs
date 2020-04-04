using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Common.Log;
using Lykke.Common.Api.Contract.Responses;
using Lykke.Common.Log;
using MAVN.Service.CustomerProfile.Auth;
using MAVN.Service.CustomerProfile.Client.Api;
using MAVN.Service.CustomerProfile.Client.Models;
using MAVN.Service.CustomerProfile.Client.Models.Enums;
using MAVN.Service.CustomerProfile.Client.Models.Requests;
using MAVN.Service.CustomerProfile.Client.Models.Responses;
using MAVN.Service.CustomerProfile.Domain.Exceptions;
using MAVN.Service.CustomerProfile.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CustomerProfileModel = Lykke.Service.CustomerProfile.Domain.Models.CustomerProfileModel;

namespace MAVN.Service.CustomerProfile.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/customers")]
    public class CustomerProfileController : ControllerBase, ICustomerProfileApi
    {
        private readonly ICustomerProfileService _customerProfileService;
        private readonly IApiKeyService _apiKeyService;
        private readonly IMapper _mapper;
        private readonly ILog _log;

        public CustomerProfileController(
            ICustomerProfileService customerProfileService,
            IApiKeyService apiKeyService,
            IMapper mapper,
            ILogFactory logFactory)
        {
            _customerProfileService = customerProfileService;
            _apiKeyService = apiKeyService;
            _mapper = mapper;
            _log = logFactory.CreateLog(this);
        }

        /// <summary>
        /// Gets info for Customer by CustomerId
        /// </summary>
        /// <returns><see cref="CustomerProfileResponse"/></returns>
        [HttpGet("{customerId}")]
        [ProducesResponseType(typeof(CustomerProfileResponse), (int)HttpStatusCode.OK)]
        public async Task<CustomerProfileResponse> GetByCustomerIdAsync(
            [Required][FromRoute] string customerId,
            [FromQuery] bool includeNotVerified,
            [FromQuery]bool includeNotActive)
        {
            if (string.IsNullOrWhiteSpace(customerId))
                throw new BadRequestException($"{nameof(customerId)} can't be empty");

            var result = await _customerProfileService.GetByCustomerIdAsync(customerId, includeNotVerified, includeNotActive);

            if (!string.IsNullOrEmpty(result?.Profile?.CustomerId))
                _log.Info(GetApiKeyName(), new { customerId = result.Profile.CustomerId });

            return _mapper.Map<CustomerProfileResponse>(result);
        }

        /// <summary>
        /// Gets paginated list of Customers
        /// </summary>
        /// <returns><see cref="PaginatedCustomerProfilesResponse"/></returns>
        [HttpGet("paginated")]
        [ProducesResponseType(typeof(CustomerProfileResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<PaginatedCustomerProfilesResponse> GetCustomersPaginatedAsync(
            [FromQuery] PaginationModel pagingInfo,
            [FromQuery] bool includeNotVerified,
            [FromQuery]bool includeNotActive)
        {
            var result = await _customerProfileService.GetPaginatedAsync(pagingInfo.CurrentPage, pagingInfo.PageSize,
                includeNotVerified, includeNotActive);

            if (result.Customers.Any())
            {
                var customerIds = result.Customers.Select(x => x.CustomerId);
                _log.Info(GetApiKeyName(), new { customerIds });
            }

            return _mapper.Map<PaginatedCustomerProfilesResponse>(result);
        }

        /// <summary>
        /// Searches for the customer profile with a given email.
        /// </summary>
        /// <param name="model">Get by email request model.</param>
        /// <returns>
        /// Customer Profile Response which holds the Profile Information about the Customer
        /// </returns>
        [HttpPost("getbyemail")]
        [ProducesResponseType(typeof(CustomerProfileResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<CustomerProfileResponse> GetByEmailAsync([FromBody] GetByEmailRequestModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Email))
                throw new BadRequestException($"{nameof(model.Email)} can't be empty");

            var result = await _customerProfileService.GetByEmailAsync(model.Email, model.IncludeNotVerified, model.IncludeNotActive);

            if (!string.IsNullOrEmpty(result?.Profile?.CustomerId))
                _log.Info(GetApiKeyName(), new { customerId = result.Profile.CustomerId });

            return _mapper.Map<CustomerProfileResponse>(result);
        }

        /// <summary>
        /// Searches for the customer profile with a given phone.
        /// </summary>
        /// <param name="model">Get by phone request model.</param>
        /// <returns></returns>
        [HttpPost("getbyphone")]
        [ProducesResponseType(typeof(CustomerProfileResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<CustomerProfileResponse> GetByPhoneAsync([FromBody] GetByPhoneRequestModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Phone))
                throw new BadRequestException($"{nameof(model.Phone)} can't be empty");

            var result = await _customerProfileService.GetByPhoneAsync(model.Phone, model.IncludeNotVerified, model.IncludeNotActive);

            if (!string.IsNullOrEmpty(result?.Profile?.CustomerId))
                _log.Info(GetApiKeyName(), new { customerId = result.Profile.CustomerId });

            return _mapper.Map<CustomerProfileResponse>(result);
        }

        /// <summary>
        /// Get collection of profiles by given collection of customer ids
        /// </summary>
        /// <param name="ids">Collection of ids</param>
        /// <param name="includeNotVerified"></param>
        /// <param name="includeNotActive"></param>
        /// <returns></returns>
        [HttpPost("list")]
        [ProducesResponseType(typeof(IEnumerable<Client.Models.Responses.CustomerProfile>), (int)HttpStatusCode.OK)]
        public async Task<IEnumerable<Client.Models.Responses.CustomerProfile>> GetByIdsAsync(
            [FromBody] string[] ids,
            [FromQuery]bool includeNotVerified,
            [FromQuery]bool includeNotActive)
        {
            var result = await _customerProfileService.GetByCustomerIdsAsync(ids, includeNotVerified, includeNotActive);

            if (result.Any())
            {
                var customerIds = result.Select(x => x.CustomerId);
                _log.Info(GetApiKeyName(), new { customerIds });
            }

            return _mapper.Map<IEnumerable<Client.Models.Responses.CustomerProfile>>(result);
        }
        
        /// <summary>
        /// Creates profile for Customer
        /// </summary>
        /// <returns><see cref="CustomerProfileResponse"/></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CustomerProfileErrorCodes), (int)HttpStatusCode.OK)]
        public async Task<CustomerProfileErrorCodes> CreateIfNotExistAsync([FromBody] CustomerProfileRequestModel customerProfile)
        {
           var result = await _customerProfileService.CreateIfNotExistsAsync(_mapper.Map<CustomerProfileModel>(customerProfile));

           return _mapper.Map<CustomerProfileErrorCodes>(result);
        }

        /// <summary>
        /// Updates customer profile.
        /// </summary>
        /// <remarks>
        /// Used to update KYA customer information.
        ///
        /// Error codes:
        /// - **CustomerProfileDoesNotExist**
        /// </remarks>
        /// <returns>
        /// 200 - customer profile successfully updated.
        /// 400 - if an invalid input data was provided
        /// </returns>
        [HttpPut]
        [ProducesResponseType(typeof(CustomerProfileErrorCodes), (int)HttpStatusCode.OK)]
        public async Task<CustomerProfileErrorCodes> UpdateAsync([FromBody] CustomerProfileUpdateRequestModel model)
        {
            var result = await _customerProfileService.UpdateAsync(model.CustomerId, model.FirstName, model.LastName,
                model.PhoneNumber, model.CountryPhoneCodeId, model.CountryOfResidenceId);
            return _mapper.Map<CustomerProfileErrorCodes>(result);
        }

        /// <summary>
        /// Updates customer email.
        /// </summary>
        /// <param name="model">The customer profile data.</param>
        /// <remarks>
        /// Updates customer email.
        ///
        /// Error codes:
        /// - **CustomerProfileDoesNotExist**
        /// </remarks>
        /// <returns>
        /// 200 - customer profile successfully updated.
        /// 400 - if an invalid input data was provided
        /// </returns>
        [HttpPut("email")]
        [ProducesResponseType(typeof(CustomerProfileErrorCodes), (int) HttpStatusCode.OK)]
        public async Task<CustomerProfileErrorCodes> UpdateEmailAsync([FromBody] EmailUpdateRequestModel model)
        {
            var result = await _customerProfileService.UpdateEmailAsync(model.CustomerId, model.Email);
            return _mapper.Map<CustomerProfileErrorCodes>(result);
        }

        /// <summary>
        /// Updates customer email.
        /// </summary>
        /// <param name="customerId">The customer id.</param>
        /// <remarks>
        /// Updates customer email.
        ///
        /// Error codes:
        /// - **CustomerProfileDoesNotExist**
        /// - **CustomerIsNotActive**
        /// </remarks>
        /// <returns>
        /// 200 - customer profile successfully updated.
        /// 400 - if an invalid input data was provided
        /// </returns>
        [HttpPut("{customerId}/deactivate")]
        [ProducesResponseType(typeof(CustomerProfileErrorCodes), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<CustomerProfileErrorCodes> RequestDeactivationAsync([Required][FromRoute] string customerId)
        {
            var result = await _customerProfileService.RequestCustomerProfileDeactivation(customerId);
            return (CustomerProfileErrorCodes)result;
        }

        /// <summary>
        /// Deletes/Archives the Customer Profile
        /// </summary>
        /// <returns><see cref="CustomerProfileResponse"/></returns>
        [HttpDelete("{customerId}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public Task DeleteAsync([Required][FromRoute] string customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
                throw new BadRequestException($"{nameof(customerId)} can't be empty");

            return _customerProfileService.RemoveAsync(customerId);
        }

        private string GetApiKeyName()
        {
            var apiKey = Request.Headers[KeyAuthOptions.DefaultHeaderName];

            if(string.IsNullOrEmpty(apiKey))
                throw new InvalidOperationException("Api Key is null");

            var apiKeyName = _apiKeyService.GetKeyName(apiKey);

            if(string.IsNullOrEmpty(apiKeyName))
                throw new InvalidOperationException("Api key name does not exists for such key");

            return apiKeyName;
        }
    }
}
