using ActiveDirectory.Extensions;
using ActiveDirectory.Models.Internal;
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, services, config) =>
    config
    .ReadFrom.Configuration(ctx.Configuration)
    .ReadFrom.Services(services));

var settings = new AppSettings();

builder.Configuration.GetSection(nameof(AppSettings)).Bind(settings);

builder.AddCors();

builder.AddCarter(settings);
builder.AddDependencies(settings);

builder.AddHealthChecks();

builder.AddOpenApi(settings);

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

app.UseOpenApi(settings);

app.UseHealthChecks();
app.UseCarter();

await app.RunAsync();
