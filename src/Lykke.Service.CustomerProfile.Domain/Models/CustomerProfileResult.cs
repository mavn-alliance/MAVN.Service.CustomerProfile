using Lykke.Service.CustomerProfile.Domain.Enums;

namespace Lykke.Service.CustomerProfile.Domain.Models
{
    public class CustomerProfileResult
    {
        public ICustomerProfile Profile { get; set; }

        public CustomerProfileErrorCodes ErrorCode { get; set; }
    }
}
