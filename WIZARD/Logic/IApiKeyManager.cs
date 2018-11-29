using System;
using System.Threading.Tasks;

namespace Logic
{
    public interface IApiKeyManager
    {
        Task<bool> ApiKeyExistAsync(Guid apiKey);
    }
}