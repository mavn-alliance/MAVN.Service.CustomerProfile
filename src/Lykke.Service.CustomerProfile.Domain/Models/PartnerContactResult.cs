using Lykke.Service.CustomerProfile.Domain.Enums;

namespace Lykke.Service.CustomerProfile.Domain.Models
{
    public class PartnerContactResult
    {
        public IPartnerContact PartnerContact { get; set; }

        public PartnerContactErrorCodes ErrorCode { get; set; }
    }
}
