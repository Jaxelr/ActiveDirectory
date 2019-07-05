# Active Directory Service

This web service is an http wrapper over basic LDAP calls to the Microsoft Active Directory service, i mostly use it on clients for quick onsite domain discovery. 

## Known Challenges & Issues

Some requests to the LDAP can be incredibly costly and take a few seconds, to mitigate this scenario an in-memory cache has been included to help with recurring requests.

- Use https for prod usage, since the content of these operations is highly sensitive in nature.

## Configurations

Some configurations that are included on the appsettings are:

1. Domains - Optional: if left empty, it will pick the current domain where the service is running. If multiple domains are defined on the Domains array, the endpoint will make requests to all of them. This was a very specific scenario that a client had. 
1. Servers - Optional: if left empty the current host is selected. Urls defined here, will be used as endpoints on the open ui page for validation.
1. Cache Configurations - Required: Cache Enabled flag that will enable/disable the cache. If enabled, the following keys must be populated.
   1. Cache Max Size - the maximum size in bytes of each cached response
   1. Cache Timespan - the time in seconds that the value will be kept alive on the cache store

The current appsettings.json can be configured manually:

```json
{
  "AppSettings": {
    "Cache": {
      "CacheTimespan": 60,
      "CacheMaxSize": 2048,
      "CacheEnabled": true
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

## HealthChecker

The endpoint of root/healthcheck for each requests includes a json heartbeat to determine if the service is online. This was done using the library of [Microsoft.AspnetCore.HealthChecks](https://github.com/dotnet-architecture/HealthChecks) for more information check the github repo.

## OpenApi

The OpenApi version used is Version 3.0.0. 
