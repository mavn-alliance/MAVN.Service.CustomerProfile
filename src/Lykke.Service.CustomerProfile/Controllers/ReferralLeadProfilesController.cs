using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Lykke.Service.CustomerProfile.Client.Api;
using Lykke.Service.CustomerProfile.Client.Models.Enums;
using Lykke.Service.CustomerProfile.Client.Models.Requests;
using Lykke.Service.CustomerProfile.Client.Models.Responses;
using Lykke.Service.CustomerProfile.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lykke.Service.CustomerProfile.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/referralLeadProfiles")]
    public class ReferralLeadProfilesController : ControllerBase, IReferralLeadProfilesApi
    {
        private readonly IReferralLeadProfileService _referralLeadProfileService;
        private readonly IMapper _mapper;

        public ReferralLeadProfilesController(IReferralLeadProfileService referralLeadProfileService, IMapper mapper)
        {
            _referralLeadProfileService = referralLeadProfileService;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns all referral lead profiles.
        /// </summary>
        /// <returns>A collection of referral lead profiles.</returns>
        /// <response code="200">A collection of referral lead profiles.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<ReferralLeadProfile>), (int) HttpStatusCode.OK)]
        public async Task<IReadOnlyList<ReferralLeadProfile>> GetAllAsync()
        {
            var referralLeadProfiles = await _referralLeadProfileService.GetAllAsync();

            return _mapper.Map<List<ReferralLeadProfile>>(referralLeadProfiles);
        }

        /// <summary>
        /// Returns referral lead profile by identifier.
        /// </summary>
        /// <param name="referralLeadId">The referral lead identifier.</param>
        /// <returns>The referral lead profile response.</returns>
        /// <remarks> 
        /// Error codes:
        /// - **ReferralLeadProfileDoesNotExist**
        /// </remarks>
        /// <response code="200">The referral lead profile response.</response>
        [HttpGet("{referralLeadId}")]
        [ProducesResponseType(typeof(ReferralLeadProfileResponse), (int) HttpStatusCode.OK)]
        public async Task<ReferralLeadProfileResponse> GetByIdAsync(Guid referralLeadId)
        {
            var referralLeadProfile = await _referralLeadProfileService.GetByIdAsync(referralLeadId);

            if (referralLeadProfile == null)
            {
                return new ReferralLeadProfileResponse
                {
                    ErrorCode = ReferralLeadProfileErrorCodes.ReferralLeadProfileDoesNotExist
                };
            }

            return new ReferralLeadProfileResponse
            {
                ErrorCode = ReferralLeadProfileErrorCodes.None,
                Data = _mapper.Map<ReferralLeadProfile>(referralLeadProfile)
            };
        }

        /// <summary>
        /// Creates new referral lead profile.
        /// </summary>
        /// <param name="request">The model that represents the referral lead profile creation information.</param>
        /// <returns>The referral lead profile response.</returns>
        /// <remarks> 
        /// Error codes:
        /// - **ReferralLeadProfileAlreadyExists**
        /// </remarks>
        /// <response code="200">The referral lead profile response.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ReferralLeadProfileResponse), (int) HttpStatusCode.OK)]
        public async Task<ReferralLeadProfileResponse> AddAsync([FromBody] ReferralLeadProfileRequest request)
        {
            var referralLeadProfile = _mapper.Map<Domain.Models.ReferralLeadProfile>(request);

            var result = await _referralLeadProfileService.AddAsync(referralLeadProfile);

            return new ReferralLeadProfileResponse
            {
                ErrorCode = _mapper.Map<ReferralLeadProfileErrorCodes>(result),
                Data = _mapper.Map<ReferralLeadProfile>(referralLeadProfile)
            };
        }

        /// <summary>
        /// Updates referral lead profile.
        /// </summary>
        /// <param name="request">The model that represents the referral lead profile update information.</param>
        /// <returns>The referral lead profile response.</returns>
        /// <remarks> 
        /// Error codes:
        /// - **ReferralLeadProfileDoesNotExist**
        /// </remarks>
        /// <response code="200">The referral lead profile response.</response>
        [HttpPut]
        [ProducesResponseType(typeof(ReferralLeadProfileResponse), (int) HttpStatusCode.OK)]
        public async Task<ReferralLeadProfileResponse> UpdateAsync([FromBody] ReferralLeadProfileRequest request)
        {
            var referralLeadProfile = _mapper.Map<Domain.Models.ReferralLeadProfile>(request);

            var result = await _referralLeadProfileService.UpdateAsync(referralLeadProfile);

            return new ReferralLeadProfileResponse
            {
                ErrorCode = _mapper.Map<ReferralLeadProfileErrorCodes>(result),
                Data = _mapper.Map<ReferralLeadProfile>(referralLeadProfile)
            };
        }

        /// <summary>
        /// Deletes referral lead profile by identifier.
        /// </summary>
        /// <param name="referralLeadId">The referral lead identifier.</param>
        /// <response code="204">The referral lead profile successfully deleted.</response>
        [HttpDelete("{referralLeadId}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        public Task DeleteAsync(Guid referralLeadId)
        {
            return _referralLeadProfileService.DeleteAsync(referralLeadId);
        }
    }
}
