namespace Lykke.Service.CustomerProfile.Domain.Enums
{
    public enum CustomerProfileErrorCodes
    {
        None,
        CustomerProfileDoesNotExist,
        CustomerProfileAlreadyExists,        
        CustomerProfileEmailAlreadyVerified,
        CustomerProfileAlreadyExistsWithDifferentProvider,
        CustomerProfilePhoneAlreadyVerified,
        CustomerProfilePhoneIsMissing,
        InvalidCountryPhoneCodeId,
        InvalidCountryOfNationalityId,
        PhoneAlreadyExists,
        CustomerIsNotActive,
        InvalidPhoneNumber,
    }
}
