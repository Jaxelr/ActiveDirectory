using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ActiveDirectory.Repositories;
using ActiveDirectoryTests.Fakes;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ActiveDirectoryTests.Unit;

public class GroupModuleTests : IDisposable
{
    private readonly HttpClient client;

    public GroupModuleTests()
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
