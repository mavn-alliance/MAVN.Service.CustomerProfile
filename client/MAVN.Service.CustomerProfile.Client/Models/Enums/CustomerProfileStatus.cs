namespace MAVN.Service.CustomerProfile.Client.Models.Enums
{
    /// <summary>
    /// Customer profile statuses
    /// </summary>
    public enum CustomerProfileStatus
    {
        /// <summary>
        /// Customer profile is active
        /// </summary>
        Active,
        /// <summary>
        /// Customer profile deactivation is in progress
        /// </summary>
        PendingDeactivation,
        /// <summary>
        /// Customer profile is deactivated
        /// </summary>
        Deactivated
    }
}
