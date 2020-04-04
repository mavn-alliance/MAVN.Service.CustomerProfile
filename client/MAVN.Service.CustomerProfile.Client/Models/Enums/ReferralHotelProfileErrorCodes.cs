using JetBrains.Annotations;

namespace MAVN.Service.CustomerProfile.Client.Models.Enums
{
    /// <summary>
    /// Represents error codes of operations with referral hotel contracts.
    /// </summary>
    [PublicAPI]
    public enum ReferralHotelProfileErrorCodes
    {
        /// <summary>
        /// No errors.
        /// </summary>
        None,

        /// <summary>
        /// The referral hotel profile does not exist.
        /// </summary>
        ReferralHotelProfileDoesNotExist,

        /// <summary>
        /// The referral hotel profile already exists.
        /// </summary>
        ReferralHotelProfileAlreadyExists
    }
}
