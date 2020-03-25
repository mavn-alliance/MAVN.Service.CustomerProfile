namespace Lykke.Service.CustomerProfile.Domain.Enums
{
    /// <summary>
    /// Represents error codes of operations with admin profile.
    /// </summary>
    public enum AdminProfileErrorCodes
    {
        /// <summary>
        /// No errors.
        /// </summary>
        None,

        /// <summary>
        /// The admin profile does not exist.
        /// </summary>
        AdminProfileDoesNotExist,

        /// <summary>
        /// The admin profile already exists.
        /// </summary>
        AdminProfileAlreadyExists
    }
}
