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
    [Route("/api/referralFriendProfiles")]
    public class ReferralFriendProfilesController : ControllerBase, IReferralFriendProfilesApi
    {
        private readonly IReferralFriendProfileService _referralFriendProfileService;
        private readonly IMapper _mapper;

        public ReferralFriendProfilesController(
            IReferralFriendProfileService referralFriendProfileService,
            IMapper mapper)
        {
            _referralFriendProfileService = referralFriendProfileService;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns all referral friend profiles.
        /// </summary>
        /// <returns>A collection of referral friend profiles.</returns>
        /// <response code="200">A collection of referral friend profiles.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<ReferralFriendProfile>), (int) HttpStatusCode.OK)]
        public async Task<IReadOnlyList<ReferralFriendProfile>> GetAllAsync()
        {
            var referralFriendProfiles = await _referralFriendProfileService.GetAllAsync();

            return _mapper.Map<List<ReferralFriendProfile>>(referralFriendProfiles);
        }

        /// <summary>
        /// Returns referral friend profile by identifier.
        /// </summary>
        /// <param name="referralFriendId">The referral friend identifier.</param>
        /// <returns>The referral friend profile response.</returns>
        /// <remarks> 
        /// Error codes:
        /// - **ReferralFriendProfileDoesNotExist**
        /// </remarks>
        /// <response code="200">The referral friend profile response.</response>
        [HttpGet("{referralFriendId}")]
        [ProducesResponseType(typeof(ReferralFriendProfileResponse), (int) HttpStatusCode.OK)]
        public async Task<ReferralFriendProfileResponse> GetByIdAsync(Guid referralFriendId)
        {
            var referralFriendProfile = await _referralFriendProfileService.GetByIdAsync(referralFriendId);

            if (referralFriendProfile == null)
            {
                return new ReferralFriendProfileResponse
                {
                    ErrorCode = ReferralFriendProfileErrorCodes.ReferralFriendProfileDoesNotExist
                };
            }

            return new ReferralFriendProfileResponse
            {
                ErrorCode = ReferralFriendProfileErrorCodes.None,
                Data = _mapper.Map<ReferralFriendProfile>(referralFriendProfile)
            };
        }

        /// <summary>
        /// Returns referral friend profile by email and referrer.
        /// </summary>
        /// <param name="request">The referral friend request.</param>
        /// <returns>The referral friend profile response.</returns>
        /// <remarks> 
        /// Error codes:
        /// - **ReferralFriendProfileDoesNotExist**
        /// </remarks>
        /// <response code="200">The referral friend profile response.</response>
        [HttpPost("byEmailAndReferrer")]
        [ProducesResponseType(typeof(ReferralFriendProfileResponse), (int)HttpStatusCode.OK)]
        public async Task<ReferralFriendProfileResponse> GetByEmailAndReferrerAsync([FromBody] ReferralFriendByEmailAndReferrerProfileRequest request)
        {
            var referralFriendProfile = await _referralFriendProfileService.GetByEmailAndReferrerAsync(request.Email, request.ReferrerId);

            if (referralFriendProfile == null)
            {
                return new ReferralFriendProfileResponse
                {
                    ErrorCode = ReferralFriendProfileErrorCodes.ReferralFriendProfileDoesNotExist
                };
            }

            return new ReferralFriendProfileResponse
            {
                ErrorCode = ReferralFriendProfileErrorCodes.None,
                Data = _mapper.Map<ReferralFriendProfile>(referralFriendProfile)
            };
        }

        /// <summary>
        /// Creates new referral friend profile.
        /// </summary>
        /// <param name="request">The model that represents the referral friend profile creation information.</param>
        /// <returns>The referral friend profile response.</returns>
        /// <remarks> 
        /// Error codes:
        /// - **ReferralFriendProfileAlreadyExists**
        /// </remarks>
        /// <response code="200">The referral friend profile response.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ReferralFriendProfileResponse), (int) HttpStatusCode.OK)]
        public async Task<ReferralFriendProfileResponse> AddAsync([FromBody] ReferralFriendProfileRequest request)
        {
            var referralFriendProfile = _mapper.Map<Domain.Models.ReferralFriendProfile>(request);

            var result = await _referralFriendProfileService.AddAsync(referralFriendProfile);

            return new ReferralFriendProfileResponse
            {
                ErrorCode = _mapper.Map<ReferralFriendProfileErrorCodes>(result),
                Data = _mapper.Map<ReferralFriendProfile>(referralFriendProfile)
            };
        }

        /// <summary>
        /// Updates referral friend profile.
        /// </summary>
        /// <param name="request">The model that represents the referral friend profile update information.</param>
        /// <returns>The referral friend profile response.</returns>
        /// <remarks> 
        /// Error codes:
        /// - **ReferralFriendProfileDoesNotExist**
        /// </remarks>
        /// <response code="200">The referral friend profile response.</response>
        [HttpPut]
        [ProducesResponseType(typeof(ReferralFriendProfileResponse), (int) HttpStatusCode.OK)]
        public async Task<ReferralFriendProfileResponse> UpdateAsync([FromBody] ReferralFriendProfileRequest request)
        {
            var referralFriendProfile = _mapper.Map<Domain.Models.ReferralFriendProfile>(request);

            var result = await _referralFriendProfileService.UpdateAsync(referralFriendProfile);

            return new ReferralFriendProfileResponse
            {
                ErrorCode = _mapper.Map<ReferralFriendProfileErrorCodes>(result),
                Data = _mapper.Map<ReferralFriendProfile>(referralFriendProfile)
            };
        }

        /// <summary>
        /// Deletes referral friend profile by identifier.
        /// </summary>
        /// <param name="referralFriendId">The referral friend identifier.</param>
        /// <response code="204">The referral friend profile successfully deleted.</response>
        [HttpDelete("{referralFriendId}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        public Task DeleteAsync(Guid referralFriendId)
        {
            return _referralFriendProfileService.DeleteAsync(referralFriendId);
        }
    }
}
