using System.Text.RegularExpressions;

namespace Lykke.Service.CustomerProfile.Models.Validation
{
    public static class Patterns
    {
        public static readonly Regex NameRegex = new Regex(@"^((?![1-9!@#$%^&*()_+{}|:\""?></,;[\]\\=~]).)+$");
    }
}
