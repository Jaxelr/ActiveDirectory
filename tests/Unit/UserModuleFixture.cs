using System.Net.Http;
using ActiveDirectory;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace ActiveDirectoryTests.Unit
{
    public class UserModuleFixture
    {
        //private readonly HttpClient client;

        //public UserModuleFixture()
        //{
        //    var featureCollection = new FeatureCollection();
        //    featureCollection.Set<IServerAddressesFeature>(new ServerAddressesFeature());

        //    var server = new TestServer(WebHost.CreateDefaultBuilder()
        //            .UseStartup<Startup>(), featureCollection
        //    );

        //    client = server.CreateClient();
        //}

        [Fact]
        public void Module_test_should_go_here()
        {
            //Arrange
            //Act
            //Assert
            Assert.True(true);
        }

    }
}
