using JetBrains.Annotations;
using Lykke.Service.CustomerProfile.Client.Models.Enums;

namespace Lykke.Service.CustomerProfile.Client.Models.Responses
{
    /// <summary>
    /// The Information that we have for given Customer Profile
    /// </summary>
    [PublicAPI]
    public class CustomerProfileResponse
    {
        /// <summary>
        /// Customer profile
        /// </summary>
        public CustomerProfile Profile { get; set; }

        /// <summary>
        /// Holds Error Codes in case there was an error
        /// </summary>
        public CustomerProfileErrorCodes ErrorCode { get; set; }
    }
}
