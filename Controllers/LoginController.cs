using System;
using Microsoft.AspNetCore.Mvc;
using csharp_core_web_api.Actions;
using System.Data;
using csharp_core_web_api.Models;
using Microsoft.Data.SqlClient;
namespace csharp_core_web_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private ILogger<LoginController> _logger;

    // private IDbConnection _dbConnection;
     private UserDbContext _userDbContext;
    public LoginController(ILogger<LoginController> logger, UserDbContext userDbContext)
    {
        _logger = logger;
        // _dbConnection = dbConnection;
        _userDbContext = userDbContext;
    }

    [HttpPost(Name = "login")]
    public async Task<IActionResult> Run([FromBody] Users user)
    {
        try
        {
            LoginAction loginAction = new( _userDbContext);
            await loginAction.Store(user);

            // if (_dbConnection is SqlConnection sqlConnection)
            // {
            //     await sqlConnection.OpenAsync();

            //     Console.WriteLine( "sqlConnection.State");
            //     Console.WriteLine( sqlConnection.State);
            //     Console.WriteLine( "sqlConnection.State   END");

            // }
            return Ok(new { Token = "fake-jwt-token" });

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(new { error = ex.Message });
        }
    }
}