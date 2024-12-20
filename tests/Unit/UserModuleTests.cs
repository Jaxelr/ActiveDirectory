using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ActiveDirectory.Models.Operations;
using ActiveDirectory.Repositories;
using ActiveDirectoryTests.Fakes;
using ActiveDirectoryTests.Mocks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace ActiveDirectoryTests.Unit;

public class UserModuleTests : IDisposable
{
    private const string ApplicationJson = "application/json";
    private readonly HttpClient client;

    public UserModuleTests()
    {
        var server = new WebApplicationFactory<Program>()
        .WithWebHostBuilder(builder => builder.ConfigureServices(services => services.AddSingleton<IAdRepository, MockAdRepository>()));
        client = server.CreateClient();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            client?.Dispose();
        }
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
        string request = JsonConvert.SerializeObject(new IsUserInGroupRequest() { Groups = [user.Group] });

        //Act
        var res = await client.PostAsync($"UserInGroup/{user.UserName}", new StringContent(request, Encoding.UTF8, ApplicationJson));
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
        var res = await client.PostAsync($"AuthenticateUser/{user.UserName}", new StringContent(request, Encoding.UTF8, ApplicationJson));

        //Assert
        Assert.Equal(HttpStatusCode.OK, res.StatusCode);
    }

    [Fact]
    public async Task Post_authenticateUser_empty_request_async()
    {
        //Arrange
        var user = new FakeUser();

        string request = JsonConvert.SerializeObject(new AuthenticUserRequest() { Password = string.Empty });

        //Act
        var res = await client.PostAsync($"AuthenticateUser/{user.UserName}", new StringContent(request, Encoding.UTF8, ApplicationJson));

        //Assert
        Assert.Equal(HttpStatusCode.UnprocessableEntity, res.StatusCode);
    }

    [Fact]
    public async Task Post_authenticateUser_invalid_request_async()
    {
        //Arrange
        var user = new FakeUser();

        //Act
        var res = await client.PostAsync($"AuthenticateUser/{user.UserName}", new StringContent(string.Empty, Encoding.UTF8, ApplicationJson));

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, res.StatusCode);
    }
}
