using ActiveDirectory.Models.Internal;
using ActiveDirectory.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ActiveDirectory.Extensions;

public static class ServiceExtensions
{
    public static WebApplicationBuilder AddDependencies(this WebApplicationBuilder builder, AppSettings settings)
    {
        builder.Services.AddSingleton(settings); //typeof(AppSettings)

        var _ = (settings.Domains.Empty()) ?
                builder.Services.AddSingleton<IAdRepository, AdRepository>() :
                builder.Services.AddSingleton<IAdRepository>(services => new AdRepository(services.GetRequiredService<ILogger<AdRepository>>(), settings.Domains));

        return builder;
    }
}
