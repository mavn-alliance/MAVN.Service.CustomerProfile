using JetBrains.Annotations;

namespace MAVN.Service.CustomerProfile.Client.Models.Enums
{
    /// <summary>
    /// Represents error codes of operations with admin profile.
    /// </summary>
    [PublicAPI]
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
