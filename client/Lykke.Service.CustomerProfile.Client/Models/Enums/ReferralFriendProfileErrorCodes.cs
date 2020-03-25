using JetBrains.Annotations;

namespace Lykke.Service.CustomerProfile.Client.Models.Enums
{
    /// <summary>
    /// Represents error codes of operations with referral friend contracts.
    /// </summary>
    [PublicAPI]
    public enum ReferralFriendProfileErrorCodes
    {
        /// <summary>
        /// No errors.
        /// </summary>
        None,

        /// <summary>
        /// The referral friend profile does not exist.
        /// </summary>
        ReferralFriendProfileDoesNotExist,

        /// <summary>
        /// The referral friend profile already exists.
        /// </summary>
        ReferralFriendProfileAlreadyExists
    }
}
