using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ApiKeyProvider : IApiKeyProvider
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder;

        public ApiKeyProvider(SqlConnectionStringBuilder sqlConnectionStringBuilder)
        {
            _sqlConnectionStringBuilder = sqlConnectionStringBuilder;
        }

        public async Task<bool> ApiKeyExistAsync(Guid apiKey)
        {
            using (ApplicationDbContext db = new ApplicationDbContext(_sqlConnectionStringBuilder))
            {
                return await db.ApiKeys.AnyAsync(x => x.Id == apiKey);
            }
        }
    }
}
