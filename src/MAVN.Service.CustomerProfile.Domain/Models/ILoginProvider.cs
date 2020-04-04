using MAVN.Service.CustomerProfile.Domain.Enums;

namespace MAVN.Service.CustomerProfile.Domain.Models
{
    public interface ILoginProvider
    {
        LoginProvider LoginProvider { get; set; }
    }
}
