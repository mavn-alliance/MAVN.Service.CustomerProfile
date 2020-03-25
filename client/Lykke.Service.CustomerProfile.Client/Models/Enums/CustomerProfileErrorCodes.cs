using JetBrains.Annotations;

namespace Lykke.Service.CustomerProfile.Client.Models.Enums
{
    /// <summary>
    /// Represents different Error Codes of operations with CustomerProfile
    /// </summary>
    [PublicAPI]
    public enum CustomerProfileErrorCodes
    {
        /// <summary>
        /// There was no error
        /// </summary>
        None,
        /// <summary>
        /// The Customer Profile does not exist
        /// </summary>
        CustomerProfileDoesNotExist,
        /// <summary>
        /// We already have Customer profile for the Customer
        /// </summary>
        CustomerProfileAlreadyExists,
        /// <summary>
        /// Customer profile email is already verified
        /// </summary>
        CustomerProfileEmailAlreadyVerified,
        /// <summary>
        /// Customer profile exists but it is registered with different login provider.
        /// For example profile is registered with Google but we are trying to do standard registration
        /// </summary>
        CustomerProfileAlreadyExistsWithDifferentProvider,
        /// <summary>
        /// Customer's phone is already verified
        /// </summary>
        CustomerProfilePhoneAlreadyVerified,
        /// <summary>
        /// Customer's phone has not been set
        /// </summary>
        CustomerProfilePhoneIsMissing,
        /// <summary>
        /// The provided Country Phone Code Id does not match any of ours
        /// </summary>
        InvalidCountryPhoneCodeId,
        /// <summary>
        /// The provided Country id does not match any of ours
        /// </summary>
        InvalidCountryOfNationalityId,
        /// <summary>
        /// There is another customer with the same verified phone number
        /// </summary>
        PhoneAlreadyExists,
        /// <summary>
        /// The customer is not active
        /// </summary>
        CustomerIsNotActive,
        /// <summary>
        /// The provided phone number is not valid
        /// </summary>
        InvalidPhoneNumber,
    }
}
