using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using Microsoft.Extensions.Logging;
using Models;

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
    [HttpPost]
    public User? LogIn(string username, string password)
    {
        //return services.RepositoryName(username, password);
        return null;
    }
}


