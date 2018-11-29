using System;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace WIZARD.Middleware
{
    public class ApiKeyMiddleware
    {
        private static string API_KEY_HEADER = "X-API-KEY";

        private readonly RequestDelegate Next;
        private readonly ILogger<CustomErrorHandlingMiddleware> Logger;

        public ApiKeyMiddleware(RequestDelegate next, ILogger<CustomErrorHandlingMiddleware> logger)
        {
            Next = next;
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context, IApiKeyManager apiKeyManager)
        {
            string apiKey = context.Request.Headers[API_KEY_HEADER].ToString();

            if (string.IsNullOrEmpty(apiKey)
                || !Guid.TryParse(apiKey, out Guid apiKeyGuid)
                || !await apiKeyManager.ApiKeyExistAsync(apiKeyGuid))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid Api Key");
                return;
            }

            await Next.Invoke(context);
        }
    }
}
