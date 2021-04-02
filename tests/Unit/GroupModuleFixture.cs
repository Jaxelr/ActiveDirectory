using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ActiveDirectory;
using ActiveDirectoryTests.Fakes;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ActiveDirectoryTests.Unit
{
    public class GroupModuleFixture : IDisposable
    {
        private readonly HttpClient client;
        private readonly TestServer server;

        public GroupModuleFixture()
        {
            var featureCollection = new FeatureCollection();
            featureCollection.Set<IServerAddressesFeature>(new ServerAddressesFeature());
            featureCollection.Set<IAdRepository>(new MockAdRepository());

            server = new TestServer(WebHost.CreateDefaultBuilder()
                    .UseStartup<Startup>()
                    .ConfigureTestServices
                    (
                        services => services.AddSingleton<IAdRepository, MockAdRepository>()
                    ),
                    featureCollection);

            client = server.CreateClient();
        }

        public void Dispose()
        {
            client?.Dispose();
            server?.Dispose();

            GC.SuppressFinalize(this);
        }

        [Fact]
        public async Task Get_userGroup_request_async()
        {
            //Arrange
            var group = new FakeUserGroup();

            //Act
            var res = await client.GetAsync($"GroupUser/{group.GroupName}");
            string response = await res.Content.ReadAsStringAsync();

            //Assert

            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
            Assert.Contains(group.GroupName, response);
        }
    }
}
