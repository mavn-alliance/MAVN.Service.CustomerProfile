using System.Net;
using System.Threading.Tasks;
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
    [Route("api/phones")]
    public class CustomerPhonesController : ControllerBase, ICustomerPhonesApi
    {
        private readonly ICustomerProfileService _customerProfileService;

        public CustomerPhonesController(ICustomerProfileService customerProfileService)
        {
            _customerProfileService = customerProfileService;
        }

        /// <summary>
        /// Set Customer's Phone info
        /// </summary>
        /// <returns><see cref="SetCustomerPhoneInfoResponseModel"/></returns>
        [HttpPost]
        [ProducesResponseType(typeof(SetCustomerPhoneInfoResponseModel), (int)HttpStatusCode.OK)]
        public async Task<SetCustomerPhoneInfoResponseModel> SetCustomerPhoneInfoAsync([FromBody] SetCustomerPhoneInfoRequestModel request)
        {
            var result = await _customerProfileService.UpdatePhoneInfoAsync(request.CustomerId, request.PhoneNumber, request.CountryPhoneCodeId);

            return new SetCustomerPhoneInfoResponseModel{ ErrorCode = (CustomerProfileErrorCodes)result };
        }

        /// <summary>
        /// Set Customer's Phone as verified
        /// </summary>
        /// <returns><see cref="VerifiedPhoneResponse"/></returns>
        [HttpPost("verify")]
        [ProducesResponseType(typeof(VerifiedPhoneResponse), (int)HttpStatusCode.OK)]
        public async Task<VerifiedPhoneResponse> SetCustomerPhoneAsVerifiedAsync([FromBody] SetPhoneAsVerifiedRequestModel request)
        {
            var result = await _customerProfileService.SetPhoneAsVerifiedAsync(request.CustomerId);

            return new VerifiedPhoneResponse{ErrorCode = (CustomerProfileErrorCodes)result };
        }
    }
}
