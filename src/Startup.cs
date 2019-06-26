using System.Collections.Generic;
using System.Threading.Tasks;
using ActiveDirectory.Entities;
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.HealthChecks;
using Microsoft.Extensions.Hosting;

namespace ActiveDirectory
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }

        private readonly AppSettings settings = new AppSettings();

        private const string ServiceName = "Active Directory";

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
              .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //HealthChecks
            services.AddHealthChecks(checks =>
            {
                checks.AddValueTaskCheck("HTTP Endpoint", () => new
                    ValueTask<IHealthCheckResult>(HealthCheckResult.Healthy("Ok")));
            });

            //Extract the AppSettings information from the appsettings config.
            Configuration.GetSection(nameof(AppSettings)).Bind(settings);

            services.AddSingleton(settings); //AppSettings type
            services.AddSingleton(settings.Cache); //CacheConfig type
            services.AddSingleton<Store>();

            if (settings.Domains.Length == 1 && string.IsNullOrEmpty(settings.Domains[0]))
                services.AddSingleton<IAdRepository>(new AdRepository());
            else
                services.AddSingleton<IAdRepository>(new AdRepository(settings.Domains));

            services.AddCarter();
            services.AddMemoryCache();
        }

        public void Configure(IApplicationBuilder app)
        {
            var addresses = app.ServerFeatures.Get<IServerAddressesFeature>().Addresses;

            app.UseCarter(GetOptions(addresses));

            app.UseSwaggerUI(opt =>
            {
                opt.RoutePrefix = RouteDefinition.RoutePrefix;
                opt.SwaggerEndpoint(RouteDefinition.SwaggerEndpoint, ServiceName);
            });
        }

        private CarterOptions GetOptions(ICollection<string> addresses) =>
            new CarterOptions(openApiOptions: new OpenApiOptions(ServiceName, addresses, new Dictionary<string, OpenApiSecurity>()));
    }
}
