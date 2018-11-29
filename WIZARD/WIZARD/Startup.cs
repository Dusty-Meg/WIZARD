using AspNet.Security.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WIZARD.Middleware;

namespace WIZARD
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            EsiSecrets esiSecrets = Configuration.GetSection("EsiSecrets").Get<EsiSecrets>();

            services.AddAuthentication().AddEVEOnline(options =>
            {
                options.Server = EVEOnlineAuthenticationServer.Tranquility;
                options.ClientId = esiSecrets.EsiClientId;
                options.ClientSecret = esiSecrets.EsiClientSecret;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables();

            app.UseMiddleware<CustomErrorHandlingMiddleware>();
            app.UseMiddleware<ApiKeyMiddleware>();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
