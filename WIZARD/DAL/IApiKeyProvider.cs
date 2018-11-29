using System;
using System.Threading.Tasks;

namespace DAL
{
    public interface IApiKeyProvider
    {
        Task<bool> ApiKeyExistAsync(Guid apiKey);
    }
}