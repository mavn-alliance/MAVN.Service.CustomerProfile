using JetBrains.Annotations;
using Lykke.Service.CustomerProfile.Client.Models.Enums;

namespace Lykke.Service.CustomerProfile.Client.Models.Responses
{
    /// <summary>
    /// The error core info given in response of setting the customer phone as verified
    /// </summary>
    [PublicAPI]
    public class SetCustomerPhoneInfoResponseModel
    {
        /// <summary>
        /// Holds Error Codes in case there was an error
        /// </summary>
        public CustomerProfileErrorCodes ErrorCode { get; set; }
    }
}
