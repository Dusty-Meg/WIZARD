using System;
using System.Threading.Tasks;
using DAL;

namespace Logic
{
    public class ApiKeyManager : IApiKeyManager
    {
        private readonly IApiKeyProvider _apiKeyProvider;

        public ApiKeyManager(IApiKeyProvider apiKeyProvider)
        {
            _apiKeyProvider = apiKeyProvider;
        }

        public async Task<bool> ApiKeyExistAsync(Guid apiKey)
        {
            return await _apiKeyProvider.ApiKeyExistAsync(apiKey);
        }
    }
}
