using System;
using Microsoft.AspNetCore.Mvc;
using csharp_core_web_api.Actions;
using System.Data;
using csharp_core_web_api.Models;
using Microsoft.Data.SqlClient;
namespace csharp_core_web_api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    // [HttpGet(Name = "Get")]
    // [ProducesResponseType(StatusCodes.Status201Created)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public async Task<IActionResult> Get()
    // {
    //     //
    // }
}