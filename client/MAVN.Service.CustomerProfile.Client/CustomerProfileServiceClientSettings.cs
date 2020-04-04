using JetBrains.Annotations;
using Lykke.SettingsReader.Attributes;

namespace MAVN.Service.CustomerProfile.Client 
{
    /// <summary>
    /// CustomerProfile client settings.
    /// </summary>
    [PublicAPI]
    public class CustomerProfileServiceClientSettings 
    {
        /// <summary>Service url.</summary>
        [HttpCheck("api/isalive")]
        public string ServiceUrl {get; set;}
    }
}
