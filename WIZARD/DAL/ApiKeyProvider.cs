using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ApiKeyProvider : IApiKeyProvider
    {
        public async Task<bool> ApiKeyExistAsync(Guid apiKey)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return await db.ApiKeys.AnyAsync(x => x.Id == apiKey);
            }
        }
    }
}
