using ActiveDirectory.Extensions;
using ActiveDirectory.Models.Internal;
using ActiveDirectory.Repositories;
using Carter;
using Carter.Cache;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, services, config) =>
    config
    .ReadFrom.Configuration(ctx.Configuration)
    .ReadFrom.Services(services));

var settings = new AppSettings();

builder.Configuration.GetSection(nameof(AppSettings)).Bind(settings);

builder.AddCors();

builder.Services.AddCarterCaching(new CachingOption(settings.Cache.CacheMaxSize));
builder.Services.AddCarter();

builder.Services.AddSingleton(settings); //typeof(AppSettings)
var _ = (settings.Domains.Empty()) ?
        builder.Services.AddSingleton<IAdRepository, AdRepository>() :
        builder.Services.AddSingleton<IAdRepository>(services => new AdRepository(services.GetRequiredService<ILogger<AdRepository>>(), settings.Domains));

builder.AddHealthChecks();

builder.AddSwagger(settings);

var app = builder.Build();

app.UseCors();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSwagger(settings);

app.UseHealthChecks();

app.UseCarterCaching();
app.MapCarter();

app.Run();
