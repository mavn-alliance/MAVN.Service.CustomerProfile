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
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task CreateOrUpdateAsync([FromBody]PartnerContactRequestModel partnerContactRequest)
        {
           await _partnerContactService.CreateOrUpdateAsync(_mapper.Map<PartnerContactModel>(partnerContactRequest));
        }

        /// <summary>
        /// Deletes/Archives the Partner contact Profile
        /// </summary>
        [HttpDelete("{locationId}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public Task DeleteIfExistAsync([Required][FromRoute] string locationId)
        {
            if (string.IsNullOrWhiteSpace(locationId))
                throw new BadRequestException($"{nameof(locationId)} can't be empty");

            return _partnerContactService.RemoveIfExistsAsync(locationId);
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
