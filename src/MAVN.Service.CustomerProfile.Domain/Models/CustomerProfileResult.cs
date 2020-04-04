using MAVN.Service.CustomerProfile.Domain.Enums;

namespace MAVN.Service.CustomerProfile.Domain.Models
{
    public class CustomerProfileResult
    {
        public ICustomerProfile Profile { get; set; }

        public CustomerProfileErrorCodes ErrorCode { get; set; }
    }
}
