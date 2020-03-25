namespace Lykke.Service.CustomerProfile.Client.Models.Requests
{
    /// <summary>
    /// Request model for getting profile by email.
    /// </summary>
    public class GetByEmailRequestModel
    {
        /// <summary>Email</summary>
        public string Email { get; set; }

        /// <summary>Include not verified customers flag.</summary>
        public bool IncludeNotVerified { get; set; }

        /// <summary>Include customers where status is not Active.</summary>
        public bool IncludeNotActive { get; set; }
    }
}
