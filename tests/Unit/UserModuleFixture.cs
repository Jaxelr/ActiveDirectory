using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ActiveDirectory.Models.Operations;
using ActiveDirectory.Repositories;
using ActiveDirectoryTests.Fakes;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace ActiveDirectoryTests.Unit
{
    public class UserModuleFixture : IDisposable
    {
        private readonly HttpClient client;

        public UserModuleFixture()
        {
            var server = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder.ConfigureServices(services => services.AddSingleton<IAdRepository, MockAdRepository>()));
            client = server.CreateClient();
        }

        public void Dispose()
        {
            client?.Dispose();

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
