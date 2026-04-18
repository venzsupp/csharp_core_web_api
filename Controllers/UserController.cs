using System;
using Microsoft.AspNetCore.Mvc;
using csharp_core_web_api.Actions;
using System.Data;
using csharp_core_web_api.Models;
// using Microsoft.Data.SqlClient;

using csharp_core_web_api.Abstracts.Exceptions;

namespace csharp_core_web_api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private UserDbContext _userDbContext;

    public UserController(ILogger<UserController> logger, UserDbContext userDbContext)
    {
        _logger = logger;
        _userDbContext = userDbContext;
    }

    [HttpPost("user")]
    public async Task<IActionResult> Store([FromBody] Users user)
    {
        try 
        {
            UserAction userAction = new(_userDbContext);
            var response = await userAction.SaveUser(user);
            return new OkObjectResult(response);
        }
        catch (Exception ex)
        {
           return new BadRequestObjectResult(ex.Message);
        }
    }

    [HttpGet("user")]
    public async Task<IActionResult> Index()
    {
        try 
        {
            UserAction userAction = new(_userDbContext);
            var resp = await userAction.GetUsers();
            // dynamic temp = new System.Dynamic.ExpandoObject();
            // temp.Id = 123;
            // temp.Status = "Pending";
            return new OkObjectResult( resp);
        }
        catch (DataAccessException ex)
        {
           return new BadRequestObjectResult(ex.Message);
        }
    }
}