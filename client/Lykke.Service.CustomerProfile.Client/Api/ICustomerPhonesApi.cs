using System.Threading.Tasks;
using Lykke.Service.CustomerProfile.Client.Models.Requests;
using Lykke.Service.CustomerProfile.Client.Models.Responses;
using Refit;

namespace Lykke.Service.CustomerProfile.Client.Api
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
