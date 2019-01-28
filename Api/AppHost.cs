using Api.Logic.Validators;
using Api.Model.ActiveDirectory.Properties;
using Funq;
using NotDeadYet;
using Repoes;
using ServiceStack.Api.Swagger;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Configuration;
using ServiceStack.ServiceInterface.Cors;
using ServiceStack.ServiceInterface.Validation;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints;
using System.Diagnostics;

public class AppHost : AppHostBase
{
    /// <summary>
    ///     Initializes a new instance of your ServiceStack application, with the specified name and assembly containing the services.
    /// </summary>
    public AppHost() : base(Default.ServiceName, typeof(AppHost).Assembly) { }

    public override void Configure(Container container)
    {
        JsConfig.DateHandler = JsonDateHandler.ISO8601;

        //Set JSON web services to return idiomatic JSON camelCase properties
        JsConfig.EmitCamelCaseNames = true;

        Plugins.Add(new CorsFeature()); //Enable CORS
        Plugins.Add(new ValidationFeature());
        Plugins.Add(new SwaggerFeature());

        SetConfig(new EndpointHostConfig
        {
#if DEBUG
            DebugMode = true,
#endif
            Return204NoContentForEmptyResponse = true,
            SoapServiceName = Default.ServiceName,
            WsdlServiceNamespace = Default.Namespace,
            WsdlSoapActionNamespace = Default.Namespace
        });

        //Health Checker
        var thisAssembly = typeof(AppHost).Assembly;
        var notDeadYetAssembly = typeof(IHealthChecker).Assembly;

        var healthChecker = new HealthCheckerBuilder()
            .WithHealthChecksFromAssemblies(thisAssembly, notDeadYetAssembly)
            .Build();

        container.Register(healthChecker);

        //Registering Validators.
        container.RegisterValidators(typeof(UserValidator).Assembly);

        //Cache time in seconds.
        string span = Default.AppSettingKeys.CacheExpiry.ToString();
        int cacheExpiry = ConfigUtils.GetAppSetting(span, 0);

        //Registerin InMemory Cache.
        container.RegisterAutoWiredAs<MemoryCacheClient, ICacheClient>();
        container.Register(new System.TimeSpan(0, 0, cacheExpiry));

        container.RegisterAutoWired<Stopwatch>();

        var domains = ConfigUtils.GetListFromAppSetting("Domain");

        if (domains.Count == 1 && domains[0] == string.Empty)
        {
            container.Register<IAdRepository>(a => new AdRepository());
        }
        else
        {
            container.Register<IAdRepository>(a => new AdRepository(domains));
        }
    }
}