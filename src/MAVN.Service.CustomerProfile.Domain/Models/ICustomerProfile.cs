using System;
using System.Collections.Generic;
using MAVN.Service.CustomerProfile.Domain.Enums;

namespace MAVN.Service.CustomerProfile.Domain.Models
{
    public interface ICustomerProfile
    {
        string CustomerId { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string PhoneNumber { get; set; }
        string ShortPhoneNumber { get; set; }
        int? CountryPhoneCodeId { get; set; }
        int? CountryOfResidenceId { get; set; }
        int? CountryOfNationalityId { get; set; }
        string TierId { get; set; }
        bool IsEmailVerified { get; set; }
        bool IsPhoneVerified { get; set; }
        DateTime Registered { get; set; }
        CustomerProfileStatus Status { get; set; }
        IList<LoginProvider> LoginProviders { get; set; }
    }
}
