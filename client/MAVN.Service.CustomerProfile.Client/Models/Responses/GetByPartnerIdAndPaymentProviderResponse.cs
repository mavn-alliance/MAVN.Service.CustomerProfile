using JetBrains.Annotations;
using MAVN.Service.CustomerProfile.Client.Models.Enums;

namespace MAVN.Service.CustomerProfile.Client.Models.Responses
{
    /// <summary>
    /// Response model
    /// </summary>
    [PublicAPI]
    public class GetByPartnerIdAndPaymentProviderResponse
    {
        /// <summary>
        /// Payment provider information
        /// </summary>
        public PaymentProviderDetails PaymentProviderDetails { get; set; }

        /// <summary>
        /// Error
        /// </summary>
        public PaymentProviderDetailsErrorCodes ErrorCode { get; set; }
    }
}
