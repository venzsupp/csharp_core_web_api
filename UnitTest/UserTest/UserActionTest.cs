using csharp_core_web_api.Models;
using csharp_core_web_api.Abstracts.Exceptions;
using csharp_core_web_api.Actions;
using Microsoft.Extensions.Configuration;
using System.IO;
using Xunit.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace UnitTest.UserTest;

public class UserActionTest
{
    private readonly IConfiguration _config;
    private readonly ITestOutputHelper _output;

    // private readonly UserDbContext _userDbContext;
    public UserActionTest( ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task SaveUser_Successfully_ReturnInt()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<UserDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        using var context = new UserDbContext(options);

        Users user = new()
        {
            UserName = "TestFName", 
            Password = "TestPassword"
        };

       
        UserAction userAction = new(context);

        // Act
        var res = await userAction.SaveUser(user);
         _output.WriteLine($"Output::{res}");

        // Assert
        Assert.Equal(1, res);
    }

    [Fact]

    public async Task SaveUser_Throw_Exception()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<UserDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        using var context = new UserDbContext(options);

        Users user = new()
        {
            UserName = null, 
            Password = "TestPassword"
        };

       
        UserAction userAction = new(context);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<DataAccessException>(() => userAction.SaveUser(user));

        // Optional: Verify custom properties or message
        Assert.Equal("Failed to save", exception.Message);
    }
}
