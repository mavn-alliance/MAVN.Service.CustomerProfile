namespace Lykke.Service.CustomerProfile.Domain.Services
{
    public interface IApiKeyService
    {
        bool ValidateKey(string apiKey);
        string GetKeyName(string apiKey);
    }
}
