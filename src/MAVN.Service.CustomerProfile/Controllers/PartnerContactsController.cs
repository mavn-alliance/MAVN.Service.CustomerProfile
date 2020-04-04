using System;
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
using MAVN.Service.CustomerProfile.Domain.Models;
using MAVN.Service.CustomerProfile.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MAVN.Service.CustomerProfile.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/partnerContacts")]
    public class PartnerContactsController : ControllerBase, IPartnerContactApi
    {
        private readonly IPartnerContactService _partnerContactService;
        private readonly IApiKeyService _apiKeyService;
        private readonly IMapper _mapper;
        private readonly ILog _log;

        public PartnerContactsController(
            IPartnerContactService partnerContactService,
            IApiKeyService apiKeyService,
            IMapper mapper,
            ILogFactory logFactory)
        {
            _partnerContactService = partnerContactService;
            _apiKeyService = apiKeyService;
            _mapper = mapper;
            _log = logFactory.CreateLog(this);
        }

        /// <summary>
        /// Gets info for Partner Contact by LocationId
        /// </summary>
        /// <returns><see cref="PartnerContactResponse"/></returns>
        [HttpGet("{locationId}")]
        [ProducesResponseType(typeof(PartnerContactResponse), (int)HttpStatusCode.OK)]
        public async Task<PartnerContactResponse> GetByLocationIdAsync([Required][FromRoute] string locationId)
        {
            if (string.IsNullOrWhiteSpace(locationId))
                throw new BadRequestException($"{nameof(locationId)} can't be empty");

            var result = await _partnerContactService.GetByLocationIdAsync(locationId);

            if (!string.IsNullOrEmpty(result?.PartnerContact?.LocationId))
                _log.Info(GetApiKeyName(), new { locationId = result.PartnerContact.LocationId});

            return _mapper.Map<PartnerContactResponse>(result);
        }

        /// <summary>
        /// Gets paginated list of PartnerContacts
        /// </summary>
        /// <returns><see cref="PaginatedPartnerContactsResponse"/></returns>
        [HttpGet("paginated")]
        [ProducesResponseType(typeof(PaginatedPartnerContactsResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<PaginatedPartnerContactsResponse> GetPartnerContactsPaginatedAsync([FromQuery] PaginationModel pagingInfo)
        {
            var result = await _partnerContactService.GetPaginatedAsync(pagingInfo.CurrentPage, pagingInfo.PageSize);

            if (result.PartnerContacts.Any())
            {
                var locationIds = result.PartnerContacts.Select(x => x.LocationId);
                _log.Info(GetApiKeyName(), new { locationIds });
            }

            return _mapper.Map<PaginatedPartnerContactsResponse>(result);
        }

        /// <summary>
        /// Creates profile for Partner contact
        /// </summary>
        /// <returns><see cref="PartnerContactErrorCodes"/></returns>
        [HttpPost]
        [ProducesResponseType(typeof(PartnerContactErrorCodes), (int)HttpStatusCode.OK)]
        public async Task<PartnerContactErrorCodes> CreateIfNotExistAsync([FromBody]PartnerContactRequestModel partnerContactRequest)
        {
           var result = await _partnerContactService.CreateIfNotExistsAsync(_mapper.Map<PartnerContactModel>(partnerContactRequest));

           return _mapper.Map<PartnerContactErrorCodes>(result);
        }

        /// <summary>
        /// Updates Partner contact profile.
        /// </summary>
        /// <remarks>
        ///
        /// Error codes:
        /// - **PartnerContactDoesNotExist**
        /// </remarks>
        /// <returns>
        /// 200 - Partner contact profile successfully updated.
        /// 400 - if an invalid input data was provided
        /// </returns>
        [HttpPut]
        [ProducesResponseType(typeof(PartnerContactErrorCodes), (int)HttpStatusCode.OK)]
        public async Task<PartnerContactErrorCodes> UpdateAsync([FromBody] PartnerContactUpdateRequestModel model)
        {
            var result = await _partnerContactService.UpdateAsync(model.LocationId, model.FirstName, model.LastName,
                model.PhoneNumber, model.Email);
            return _mapper.Map<PartnerContactErrorCodes>(result);
        }

        /// <summary>
        /// Deletes/Archives the Partner contact Profile
        /// </summary>
        [HttpDelete("{locationId}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public Task DeleteAsync([Required][FromRoute] string locationId)
        {
            if (string.IsNullOrWhiteSpace(locationId))
                throw new BadRequestException($"{nameof(locationId)} can't be empty");

            return _partnerContactService.RemoveAsync(locationId);
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
