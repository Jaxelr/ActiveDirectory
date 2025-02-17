# Active Directory Service

This web service is an http wrapper over basic LDAP calls to the Microsoft Active Directory service, I mostly use it on clients for quick onsite domain discovery.

| Ci  | Status | Branch |
| :---: | :---: | :---: |
| Azure Pipelines | [![Build Status][azure-main-img]][azure-main] | master |
| Github Actions | [![.NET][github-main-img]][github-main] | master |


## Known Challenges & Issues

Some requests to the LDAP can be costly and take a few seconds, to mitigate this scenario an in-memory cache has been included to help with recurring requests.

- Use https for prod usage, since the content of these operations is highly sensitive in nature.

## Configurations

Some configurations that are included on the appsettings are:

1. Domains - Optional: if left empty, it will pick the current domain where the service is running. If multiple domains are defined on the Domains array, the endpoint will make requests to all of them. This was a very specific scenario that a client had.
1. Addresses - Optional: if left empty the current host is selected. Urls defined here, will be used as endpoints on the open ui page for validation.
1. Route Definition - Required: These values are attached to the openapi declaration and are needed for the defined metadata info
   1. Route Suffix - The path where the swagger json will be shown.
   1. Version - The swagger.json file version.
1. Cache Configurations - Required: Cache Enabled flag that will enable/disable the cache. If enabled, the following keys must be populated.
   1. Cache Max Size - the maximum size in bytes of each cached response
   1. Cache Timespan - the time in seconds that the value will be kept alive on the cache store
   1. Cache Enabled - boolean to activate or deactivate cache on startup

The current appsettings.json can be configured manually:

```json
{
  "Serilog": {
    "Using": [ "Serilog.Sinks.Console" ],
    "MinimumLevel": "Information",
    "WriteTo": [
      { "Name": "Console" }
    ],
    "Enrich": [ "FromLogContext", "WithMachineName", "WithThreadId" ],
  },
  "AllowedHosts": "*",
  "AppSettings": {
    "Cache": {
      "CacheTimespan": 60,
      "CacheMaxSize": 2048,
      "CacheEnabled": true
    },
    "RouteDefinition": {
      "RouteSuffix": "/swagger",
      "Version": "v1"
    },
    "Domains": [
      ""
    ],
    "Addresses": [
      ""
    ]
  }
}
```

## Health Check Feature

The endpoint of root/healthcheck for each requests includes a json heartbeat to determine if the service is online. This was done using the library of [Microsoft.Extensions.Diagnostics.HealthChecks](https://github.com/dotnet/aspnetcore/tree/main/src/HealthChecks) for more information check the github repo.

## OpenApi

The OpenApi version used is Version 3.0.1

## Dependencies & Libraries

This project targets net 8.0. For previous versions, check the tags. The following oss libraries are used on this repo as dependencies:

- [Carter](https://github.com/CarterCommunity/Carter)
- [Xunit](https://github.com/xunit/xunit)
- [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
- [Serilog.AspNetCore](https://github.com/serilog/serilog-aspnetcore/)

[github-main-img]: https://github.com/Jaxelr/ActiveDirectory/actions/workflows/ci.yml/badge.svg
[github-main]: https://github.com/Jaxelr/ActiveDirectory/actions/workflows/ci.yml
[azure-main-img]: https://dev.azure.com/jaxelr0433/ActiveDirectoryService/_apis/build/status/Jaxelr.ActiveDirectory?branchName=master
[azure-main]: https://dev.azure.com/jaxelr0433/ActiveDirectoryService/_build/latest?definitionId=2&branchName=master
