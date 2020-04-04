using MAVN.Service.CustomerProfile.Domain.Enums;

namespace MAVN.Service.CustomerProfile.Domain.Models
{
    public class PartnerContactResult
    {
        public IPartnerContact PartnerContact { get; set; }

        public PartnerContactErrorCodes ErrorCode { get; set; }
    }
}
