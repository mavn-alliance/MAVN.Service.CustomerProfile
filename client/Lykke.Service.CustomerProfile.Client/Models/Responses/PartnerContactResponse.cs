using JetBrains.Annotations;
using Lykke.Service.CustomerProfile.Client.Models.Enums;

namespace Lykke.Service.CustomerProfile.Client.Models.Responses
{
    /// <summary>
    /// The Information that we have for given Contact Person Profile
    /// </summary>
    [PublicAPI]
    public class PartnerContactResponse
    {
        /// <summary>
        /// Contact person profile
        /// </summary>
        public PartnerContact PartnerContact { get; set; }

        /// <summary>
        /// Holds Error Codes in case there was an error
        /// </summary>
        public PartnerContactErrorCodes ErrorCode { get; set; }
    }
}
