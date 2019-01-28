# Active Directory Service

This ws includes LDAP configuration pertaining to the MS AD for onsite utilization. 

## Known Issues

Some requests to the LDAP can be very costly and take a few seconds, to mitigate this scenario the onmemory cache has been included to help multiple requests.

The inmemory cache courtesy of ServiceStack can be configured the following way:

```xml
    <Api.Properties.Settings>
      <setting name="CacheEnabled" serializeAs="String">
        <value>True</value> <!-- Put to false to disable cache -->
      </setting>
    </Api.Properties.Settings>
```
And also allows to configure the expiry length:

```xml
      <setting name="CacheExpiry" serializeAs="String">
        <value>1800</value> <!-- in seconds -->
      </setting>
```

## HealthChecker

The endpoint of root/healthcheck for each requests includes a json heartbeat to determine if the service is online. This was done using the library of [NotDeadYet](https://github.com/uglybugger/NotDeadYet) for more information check the github repo.


## Swagger

The Swagger version used is Version 1.2. 