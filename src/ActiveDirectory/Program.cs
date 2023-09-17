using System.Text.Json;
using System.Threading.Tasks;
using ActiveDirectory.Extensions;
using ActiveDirectory.Models.Internal;
using ActiveDirectory.Repositories;
using Carter;
using Carter.Cache;
using Carter.OpenApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;

const string ServiceName = "Active Directory";
const string Policy = "DefaultPolicy";

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, services, config) =>
    config
    .ReadFrom.Configuration(ctx.Configuration)
    .ReadFrom.Services(services));

var settings = new AppSettings();

builder.Configuration.GetSection(nameof(AppSettings)).Bind(settings);

builder.Services.AddCors(options =>
{
    options.AddPolicy(Policy,
    builder =>
    {
        builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

builder.Services.AddCarterCaching(new CachingOption(settings.Cache.CacheMaxSize));
builder.Services.AddCarter();

builder.Services.AddSingleton(settings); //typeof(AppSettings)
var _ = (settings.Domains.Empty()) ?
        builder.Services.AddSingleton<IAdRepository, AdRepository>() :
        builder.Services.AddSingleton<IAdRepository>(services => new AdRepository(services.GetRequiredService<ILogger<AdRepository>>(), settings.Domains));

//HealthChecks
builder.Services.AddHealthChecks();

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(settings.RouteDefinition.Version, new OpenApiInfo
    {
        Description = ServiceName,
        Version = settings.RouteDefinition.Version,
    });

    options.DocInclusionPredicate((_, description) =>
    {
        foreach (object metaData in description.ActionDescriptor.EndpointMetadata)
        {
            if (metaData is IIncludeOpenApi)
            {
                return true;
            }
        }
        return false;
    });
});

var app = builder.Build();

app.UseCors(Policy);

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

app.UseSwagger();
app.UseSwaggerUI();

app.UseHealthChecks("/healthcheck", new HealthCheckOptions()
{
    ResponseWriter = WriteResponse
});

app.UseCarterCaching();
app.MapCarter();

app.Run();

static Task WriteResponse(HttpContext context, HealthReport report)
{
    context.Response.ContentType = "application/json";

    var json = new
    {
        statusCode = report.Status,
        status = report.Status.ToString(),
        timelapsed = report.TotalDuration
    };

    return context.Response.WriteAsync(JsonSerializer.Serialize(json));
}
