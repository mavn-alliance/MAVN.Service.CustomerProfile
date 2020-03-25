namespace Lykke.Service.CustomerProfile.Domain.Enums
{
    /// <summary>
    /// Represents error codes of operations with referral hotel contracts.
    /// </summary>
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
