using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WIZARD.Middleware
{
    public class CustomErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomErrorHandlingMiddleware> _logger;

        public CustomErrorHandlingMiddleware(RequestDelegate next, ILogger<CustomErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context)
        {
            // https://www.devtrends.co.uk/blog/handling-errors-in-asp.net-core-web-api
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                //if (ex is RootObjectNotFoundException)
                //{
                //    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                //}
                //else if (ex is ChildObjectNotFoundException)
                //{
                //    context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                //}
                //// TODO : RuleViolationException needs more handling work, it's an early notion and will need to return a nested JSON structure and not just a string message.
                //else if (ex is RuleViolationException)
                //{
                //    context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                //}
                //else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }

                var json = JsonConvert.SerializeObject(new { ex.Message }, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
                await context.Response.WriteAsync(json);
            }
        }
    }
}
