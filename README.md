# Active Directory Service

This ws includes LDAP configuration pertaining to the MS AD for onsite utilization. 

## Known Challenges

Some requests to the LDAP can be incredibly costly and take a few seconds, to mitigate this scenario the onmemory cache has been included to help with recurring requests.

## Configurations

Some configurations that are included on the appsettings are:

1. Domains - Optional: if left empty, it will pick the domain where the service is running. If multiple domains are defined on the Domains array, the endpoint will make requests to all of them. This was a very specific scenario that a client had. 
1. Servers - Optional: if left empty the current host is selected. Urls defined here, will be used as endpoints on the open ui for validation.
1. Cache Configurations - Required: Cache Enabled flag that will enable/disable the cache. If enabled, the following keys must be populated.
1. Cache Max Size - the maximum size in bytes of each cached respons
1. Cache Timespan - the time in seconds that the value will be kept alive

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
