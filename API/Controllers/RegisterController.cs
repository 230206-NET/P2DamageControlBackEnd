using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using Microsoft.Extensions.Logging;
using Models;
using Data;
using System.Text.Json;

namespace API.Controllers;

public class RegisterController : Controller
{
    private readonly ILogger<RegisterController> _logger;


    public RegisterController(ILogger<RegisterController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View("Register");
    }

    public IActionResult Privacy()
    {
        return View();
    }
    [HttpPost]
    public User? LogIn([FromBody] JsonElement newUser)
    {
        User? LogIn = JsonSerializer.Deserialize<User?>(newUser.GetRawText());
        return DBRepository.CreateNewUser(LogIn);
    }
}


