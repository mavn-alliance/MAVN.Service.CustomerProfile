using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using MAVN.Service.CustomerProfile.Client.Api;
using MAVN.Service.CustomerProfile.Client.Models.Enums;
using MAVN.Service.CustomerProfile.Client.Models.Requests;
using MAVN.Service.CustomerProfile.Client.Models.Responses;
using MAVN.Service.CustomerProfile.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MAVN.Service.CustomerProfile.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/adminProfiles")]
    public class AdminProfilesController : ControllerBase, IAdminProfilesApi
    {
        private readonly IAdminProfileService _adminProfileService;
        private readonly IMapper _mapper;

        public AdminProfilesController(IAdminProfileService adminProfileService, IMapper mapper)
        {
            _adminProfileService = adminProfileService;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns admin profiles.
        /// </summary>
        /// <param name="identifiers">The list of identifiers to be filtered.</param>
        /// <returns>A collection of admin profiles.</returns>
        /// <response code="200">A collection of admin profiles.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<AdminProfile>), (int) HttpStatusCode.OK)]
        public async Task<IReadOnlyList<AdminProfile>> GetAsync([FromQuery] Guid[] identifiers)
        {
            var adminProfiles = identifiers == null || identifiers.Length == 0
                ? await _adminProfileService.GetAllAsync()
                : await _adminProfileService.GetAsync(identifiers);

            return _mapper.Map<List<AdminProfile>>(adminProfiles);
        }

        /// <summary>
        /// Returns admin profile by identifier.
        /// </summary>
        /// <param name="adminId">The admin identifier.</param>
        /// <returns>The admin profile response.</returns>
        /// <remarks> 
        /// Error codes:
        /// - **AdminProfileDoesNotExist**
        /// </remarks>
        /// <response code="200">The admin profile response.</response>
        [HttpGet("{adminId}")]
        [ProducesResponseType(typeof(AdminProfileResponse), (int) HttpStatusCode.OK)]
        public async Task<AdminProfileResponse> GetByIdAsync(Guid adminId)
        {
            var adminProfile = await _adminProfileService.GetByIdAsync(adminId);

            if (adminProfile == null)
            {
                return new AdminProfileResponse {ErrorCode = AdminProfileErrorCodes.AdminProfileDoesNotExist};
            }

            return new AdminProfileResponse
            {
                ErrorCode = AdminProfileErrorCodes.None, Data = _mapper.Map<AdminProfile>(adminProfile)
            };
        }

        /// <summary>
        /// Creates new admin profile.
        /// </summary>
        /// <param name="request">The model that represents the admin profile creation information.</param>
        /// <returns>The admin profile response.</returns>
        /// <remarks> 
        /// Error codes:
        /// - **AdminProfileAlreadyExists**
        /// </remarks>
        /// <response code="200">The admin profile response.</response>
        [HttpPost]
        [ProducesResponseType(typeof(AdminProfileResponse), (int) HttpStatusCode.OK)]
        public async Task<AdminProfileResponse> AddAsync([FromBody] AdminProfileRequest request)
        {
            var adminProfile = _mapper.Map<Domain.Models.AdminProfile>(request);

            var result = await _adminProfileService.AddAsync(adminProfile);

            return new AdminProfileResponse
            {
                ErrorCode = _mapper.Map<AdminProfileErrorCodes>(result),
                Data = _mapper.Map<AdminProfile>(adminProfile)
            };
        }

        /// <summary>
        /// Updates admin profile.
        /// </summary>
        /// <param name="request">The model that represents the admin profile update information.</param>
        /// <returns>The admin profile response.</returns>
        /// <remarks> 
        /// Error codes:
        /// - **AdminProfileDoesNotExist**
        /// </remarks>
        /// <response code="200">The admin profile response.</response>
        [HttpPut]
        [ProducesResponseType(typeof(AdminProfileResponse), (int) HttpStatusCode.OK)]
        public async Task<AdminProfileResponse> UpdateAsync([FromBody] AdminProfileRequest request)
        {
            var adminProfile = _mapper.Map<Domain.Models.AdminProfile>(request);

            var result = await _adminProfileService.UpdateAsync(adminProfile);

            return new AdminProfileResponse
            {
                ErrorCode = _mapper.Map<AdminProfileErrorCodes>(result),
                Data = _mapper.Map<AdminProfile>(adminProfile)
            };
        }

        /// <summary>
        /// Deletes admin profile by identifier.
        /// </summary>
        /// <param name="adminId">The admin identifier.</param>
        /// <response code="204">The admin profile successfully deleted.</response>
        [HttpDelete("{adminId}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        public Task DeleteAsync(Guid adminId)
        {
            return _adminProfileService.DeleteAsync(adminId);
        }
    }
}
