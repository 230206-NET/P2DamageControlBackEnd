using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using Microsoft.Extensions.Logging;
using Models;

namespace API.Controllers;

public class TicketFormController : Controller
{
    private readonly ILogger<TicketFormController> _logger;

    public TicketFormController(ILogger<TicketFormController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View("TicketForm");
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

