using JetBrains.Annotations;

namespace Lykke.Service.CustomerProfile.Client.Models.Enums
{
    /// <summary>
    /// Represents different Error Codes of operations with Partner Contact
    /// </summary>
    [PublicAPI]
    public enum PartnerContactErrorCodes
    {
        /// <summary>
        /// There was no error
        /// </summary>
        None,
        /// <summary>
        /// The partner contact does not exist
        /// </summary>
        PartnerContactDoesNotExist,
        /// <summary>
        /// We already have partner contact for the Location
        /// </summary>
        PartnerContactAlreadyExists
    }
}
