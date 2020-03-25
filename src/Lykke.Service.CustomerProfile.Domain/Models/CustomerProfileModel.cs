using System;
using System.Collections.Generic;
using Lykke.Service.CustomerProfile.Domain.Enums;

namespace Lykke.Service.CustomerProfile.Domain.Models
{
    public class CustomerProfileModel : ICustomerProfile
    {
        public string CustomerId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string ShortPhoneNumber { get; set; }
        public int? CountryPhoneCodeId { get; set; }
        public int? CountryOfNationalityId { get; set; }
        public int? CountryOfResidenceId { get; set; }
        public string TierId { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool IsPhoneVerified { get; set; }
        public DateTime Registered { get; set; }
        public CustomerProfileStatus Status { get; set; }
        public IList<LoginProvider> LoginProviders { get; set; }
    }
}
