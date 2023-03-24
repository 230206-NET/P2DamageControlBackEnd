using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using Microsoft.Extensions.Logging;
using Models;
using Data;
using System.Text.Json;
using Services;
using Serilog;
namespace API.Controllers;

public class RegisterController : Controller
{
    private readonly ILogger<RegisterController> _logger;
    private readonly AccountService _service;


    public RegisterController(ILogger<RegisterController> logger, AccountService service)
    {
        _logger = logger;
        _service = service;
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("../logs/registerLogs.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }

    [HttpPost]
    public IActionResult Register([FromBody] User? newUser)
    {
        if (newUser == null)
        {
            return BadRequest("Invalid client request");
        }
        Log.Information($"User successfully registered an account Id: {newUser.Id}, Username: {newUser.Username}, Access Level: {newUser.AccessLevel}");
        Log.CloseAndFlush();
        return Created("Register/Register", _service.CreateNewUser(newUser));
    }
}


