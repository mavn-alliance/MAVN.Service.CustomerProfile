namespace Lykke.Service.CustomerProfile.Client.Models.Enums
{
    /// <summary>
    /// All the available types of customer profiles.
    /// Standard means profile created from our system.
    /// </summary>
    public enum LoginProvider
    {
        /// <summary>
        /// Indicates that the standard authentication provider should be used.
        /// </summary>
        Standard,
        
        /// <summary>
        /// Indicates that the Google authentication provider should be used.
        /// </summary>
        Google
    }
}
