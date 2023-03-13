using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using Microsoft.Extensions.Logging;
using Models;
using Data;
using System.Text.Json;

namespace API.Controllers;

public class NewLogInController : Controller
{
    private readonly ILogger<NewLogInController> _logger;


    public NewLogInController(ILogger<NewLogInController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View("NewLogin");
    }

    public IActionResult Privacy()
    {
        return View();
    }
   /* [HttpPost]
    public User? LogIn([FromBody] JsonElement userLogin)
    {
        User? user = JsonSerializer.Deserialize<User?>(userLogin.GetRawText());
        return DBRepository.GetUserByUsername(user);
    }*/
}


