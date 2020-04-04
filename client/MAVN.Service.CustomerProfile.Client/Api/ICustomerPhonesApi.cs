using System.Threading.Tasks;
using MAVN.Service.CustomerProfile.Client.Models.Requests;
using MAVN.Service.CustomerProfile.Client.Models.Responses;
using Refit;

namespace MAVN.Service.CustomerProfile.Client.Api
{
    /// <summary>
    /// CustomerPhones API interface
    /// </summary>
    public interface ICustomerPhonesApi
    {
        /// <summary>
        /// Set Customer's Phone info
        /// </summary>
        /// <returns><see cref="SetCustomerPhoneInfoResponseModel"/></returns>
        [Post("/api/phones")]
        Task<SetCustomerPhoneInfoResponseModel> SetCustomerPhoneInfoAsync(SetCustomerPhoneInfoRequestModel request);

        /// <summary>
        /// Set Customer's Phone as verified
        /// </summary>
        /// <returns><see cref="VerifiedPhoneResponse"/></returns>
        [Post("/api/phones/verify")]
        Task<VerifiedPhoneResponse> SetCustomerPhoneAsVerifiedAsync(SetPhoneAsVerifiedRequestModel request);
    }
}
