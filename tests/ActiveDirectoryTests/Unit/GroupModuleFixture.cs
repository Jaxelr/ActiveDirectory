﻿using System.Net.Http;
using ActiveDirectory;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.TestHost;

namespace ActiveDirectoryTests.Unit
{
    public class GroupModuleFixture
    {
        private readonly HttpClient client;

        public GroupModuleFixture()
        {
            var featureCollection = new FeatureCollection();
            featureCollection.Set<IServerAddressesFeature>(new ServerAddressesFeature());

            var server = new TestServer(WebHost.CreateDefaultBuilder()
                    .UseStartup<Startup>(), featureCollection
            );

            client = server.CreateClient();
        }
    }
}
