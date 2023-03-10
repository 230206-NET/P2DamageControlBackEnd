using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using Microsoft.Extensions.Logging;
using Models;
using Data;

namespace API.Controllers;

public class ViewAllTicketsController : Controller
{
    private readonly ILogger<ViewAllTicketsController> _logger;

    public ViewAllTicketsController(ILogger<ViewAllTicketsController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewBag.CurrentCategory = GetAllClaims();
        return View("ViewAllTickets");
    }

    public IActionResult Privacy()
    {
        return View();
    }
    [HttpGet]
    public List<Ticket> GetAllClaims()
    {
        
        //return services.RepositoryName(username, password);
        List<Ticket> tickets = new DBRepository().GetAllTickets();
        return tickets;
    }
}
