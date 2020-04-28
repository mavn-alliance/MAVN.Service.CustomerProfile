using JetBrains.Annotations;

namespace MAVN.Service.CustomerProfile.Client.Models.Enums
{
    /// <summary>
    /// Enumeration that holds error codes for payment provider details operations
    /// </summary>
    [PublicAPI]
    public enum PaymentProviderDetailsErrorCodes
    {
        /// <summary>
        /// No errors
        /// </summary>
        None,
        /// <summary>
        /// Payment provider details does not exist
        /// </summary>
        PaymentProviderDetailsDoesNotExist,
        /// <summary>
        /// There is already existing unique pair for this partner and provider
        /// </summary>
        PaymentProviderDetailsAlreadyExists
    }
}
