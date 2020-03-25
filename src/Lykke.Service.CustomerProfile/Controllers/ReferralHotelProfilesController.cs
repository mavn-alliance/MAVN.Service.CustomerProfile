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
    [Route("/api/referralHotelProfiles")]
    public class ReferralHotelProfilesController : ControllerBase, IReferralHotelProfilesApi
    {
        private readonly IReferralHotelProfileService _referralHotelProfileService;
        private readonly IMapper _mapper;

        public ReferralHotelProfilesController(IReferralHotelProfileService referralHotelProfileService, IMapper mapper)
        {
            _referralHotelProfileService = referralHotelProfileService;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns all referral hotel profiles.
        /// </summary>
        /// <returns>A collection of referral hotel profiles.</returns>
        /// <response code="200">A collection of referral hotel profiles.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<ReferralHotelProfile>), (int) HttpStatusCode.OK)]
        public async Task<IReadOnlyList<ReferralHotelProfile>> GetAllAsync()
        {
            var referralHotelProfiles = await _referralHotelProfileService.GetAllAsync();

            return _mapper.Map<List<ReferralHotelProfile>>(referralHotelProfiles);
        }

        /// <summary>
        /// Returns referral hotel profile by identifier.
        /// </summary>
        /// <param name="referralHotelId">The referral hotel identifier.</param>
        /// <returns>The referral hotel profile response.</returns>
        /// <remarks> 
        /// Error codes:
        /// - **ReferralHotelProfileDoesNotExist**
        /// </remarks>
        /// <response code="200">The referral hotel profile response.</response>
        [HttpGet("{referralHotelId}")]
        [ProducesResponseType(typeof(ReferralHotelProfileResponse), (int) HttpStatusCode.OK)]
        public async Task<ReferralHotelProfileResponse> GetByIdAsync(Guid referralHotelId)
        {
            var referralHotelProfile = await _referralHotelProfileService.GetByIdAsync(referralHotelId);

            if (referralHotelProfile == null)
            {
                return new ReferralHotelProfileResponse
                {
                    ErrorCode = ReferralHotelProfileErrorCodes.ReferralHotelProfileDoesNotExist
                };
            }

            return new ReferralHotelProfileResponse
            {
                ErrorCode = ReferralHotelProfileErrorCodes.None,
                Data = _mapper.Map<ReferralHotelProfile>(referralHotelProfile)
            };
        }

        /// <summary>
        /// Creates new referral hotel profile.
        /// </summary>
        /// <param name="request">The model that represents the referral hotel profile creation information.</param>
        /// <returns>The referral hotel profile response.</returns>
        /// <remarks> 
        /// Error codes:
        /// - **ReferralHotelProfileAlreadyExists**
        /// </remarks>
        /// <response code="200">The referral hotel profile response.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ReferralHotelProfileResponse), (int) HttpStatusCode.OK)]
        public async Task<ReferralHotelProfileResponse> AddAsync([FromBody] ReferralHotelProfileRequest request)
        {
            var referralHotelProfile = _mapper.Map<Domain.Models.ReferralHotelProfile>(request);

            var result = await _referralHotelProfileService.AddAsync(referralHotelProfile);

            return new ReferralHotelProfileResponse
            {
                ErrorCode = _mapper.Map<ReferralHotelProfileErrorCodes>(result),
                Data = _mapper.Map<ReferralHotelProfile>(referralHotelProfile)
            };
        }

        /// <summary>
        /// Updates referral hotel profile.
        /// </summary>
        /// <param name="request">The model that represents the referral hotel profile update information.</param>
        /// <returns>The referral hotel profile response.</returns>
        /// <remarks> 
        /// Error codes:
        /// - **ReferralHotelProfileDoesNotExist**
        /// </remarks>
        /// <response code="200">The referral hotel profile response.</response>
        [HttpPut]
        [ProducesResponseType(typeof(ReferralHotelProfileResponse), (int) HttpStatusCode.OK)]
        public async Task<ReferralHotelProfileResponse> UpdateAsync([FromBody] ReferralHotelProfileRequest request)
        {
            var referralHotelProfile = _mapper.Map<Domain.Models.ReferralHotelProfile>(request);

            var result = await _referralHotelProfileService.UpdateAsync(referralHotelProfile);

            return new ReferralHotelProfileResponse
            {
                ErrorCode = _mapper.Map<ReferralHotelProfileErrorCodes>(result),
                Data = _mapper.Map<ReferralHotelProfile>(referralHotelProfile)
            };
        }

        /// <summary>
        /// Deletes referral hotel profile by identifier.
        /// </summary>
        /// <param name="referralHotelId">The referral hotel identifier.</param>
        /// <response code="204">The referral hotel profile successfully deleted.</response>
        [HttpDelete("{referralHotelId}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        public Task DeleteAsync(Guid referralHotelId)
        {
            return _referralHotelProfileService.DeleteAsync(referralHotelId);
        }
    }
}
