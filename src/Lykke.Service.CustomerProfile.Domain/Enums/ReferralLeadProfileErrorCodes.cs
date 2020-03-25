namespace Lykke.Service.CustomerProfile.Domain.Enums
{
    /// <summary>
    /// Represents error codes of operations with referral lead contracts.
    /// </summary>
    public enum ReferralLeadProfileErrorCodes
    {
        /// <summary>
        /// No errors.
        /// </summary>
        None,

        /// <summary>
        /// The referral lead profile does not exist.
        /// </summary>
        ReferralLeadProfileDoesNotExist,

        /// <summary>
        /// The referral lead profile already exists.
        /// </summary>
        ReferralLeadProfileAlreadyExists
    }
}
