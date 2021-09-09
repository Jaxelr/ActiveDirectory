using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ActiveDirectory;
using ActiveDirectory.Models.Operations;
using ActiveDirectoryTests.Fakes;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace ActiveDirectoryTests.Unit
{
    public class UserModuleFixture : IDisposable
    {
        private readonly HttpClient client;

        private readonly TestServer server;

        public UserModuleFixture()
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
        public async Task Get_user_request_async()
        {
            //Arrange
            var user = new FakeUser();

            //Act
            var res = await client.GetAsync($"User/{user.UserName}");
            string response = await res.Content.ReadAsStringAsync();

            //Assert

            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
            Assert.Contains(user.UserName, response);
        }

        [Fact]
        public async Task Get_userGroup_request_async()
        {
            //Arrange
            var user = new FakeUser();

            //Act
            var res = await client.GetAsync($"UserGroup/{user.UserName}");
            string response = await res.Content.ReadAsStringAsync();

            //Assert

            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
            Assert.Contains(user.Group, response);
        }

        [Fact]
        public async Task Get_isUserInGroup_request_async()
        {
            //Arrange
            var user = new FakeUser();

            //Act
            var res = await client.GetAsync($"UserInGroup/{user.UserName}/{user.Group}");
            string response = await res.Content.ReadAsStringAsync();

            //Assert

            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
            Assert.Contains(user.Group, response);
        }

        [Fact]
        public async Task Post_authenticateUser_request_async()
        {
            //Arrange
            var user = new FakeUser();
            string request = JsonConvert.SerializeObject(new AuthenticUserRequest() { Password = FakeUser.Password });

            //Act
            var res = await client.PostAsync($"AuthenticateUser/{user.UserName}", new StringContent(request, Encoding.UTF8, "application/json"));

            //Assert
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }

        [Fact]
        public async Task Post_authenticateUser_invalid_request_async()
        {
            //Arrange
            var user = new FakeUser();

            //Act
            var res = await client.PostAsync($"AuthenticateUser/{user.UserName}", new StringContent(""));

            //Assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, res.StatusCode);
        }
    }
}
