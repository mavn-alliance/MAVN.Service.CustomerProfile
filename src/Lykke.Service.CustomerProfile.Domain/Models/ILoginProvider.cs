using Lykke.Service.CustomerProfile.Domain.Enums;

namespace Lykke.Service.CustomerProfile.Domain.Models
{
    public interface ILoginProvider
    {
        LoginProvider LoginProvider { get; set; }
    }
}
