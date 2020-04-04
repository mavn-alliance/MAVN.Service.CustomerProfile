namespace MAVN.Service.CustomerProfile.Client.Models.Requests
{
    /// <summary>
    /// Request model for getting profile by phone.
    /// </summary>
    public class GetByPhoneRequestModel
    {
        /// <summary>Phone</summary>
        public string Phone { get; set; }

        /// <summary>Include not verified customers flag.</summary>
        public bool IncludeNotVerified { get; set; }

        /// <summary>Include customers where status is not Active.</summary>
        public bool IncludeNotActive { get; set; }
    }
}
