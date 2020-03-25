namespace Lykke.Service.CustomerProfile.Client.Models.Constants
{
    /// <summary>
    /// Class which holds validations constants for the client models
    /// </summary>
    public static class ValidationConstants
    {
        /// <summary>
        /// Regular expression for name validation
        /// </summary>
        public const string NameValidationPattern = @"(^([a-zA-Z]+[\- ]?)*[a-zA-Z'’]+$)|(^([\u0600-\u065F\u066A-\u06EF\u06FA-\u06FF]+[\- ]?)*[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FF]+$)";

        /// <summary>
        /// Regular expression for phone validation
        /// </summary>
        public const string PhoneValidationPattern = "^[0-9]+$";

        /// <summary>
        /// Regular expression for email validation
        /// </summary>
        public const string EmailValidationPattern =
            @"\A(?:[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?)\Z";
    }
}
