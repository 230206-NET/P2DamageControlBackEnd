using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Models;
using Data;
using Services;

namespace API.Controllers;

public class TicketFormController : Controller
{
    private readonly ILogger<TicketFormController> _logger;
    private readonly AccountService _service;
    private readonly IRepository _dbrepository;

    public TicketFormController(ILogger<TicketFormController> logger)
    {
        _logger = logger;
        _dbrepository = new DBRepository();
        _service = new AccountService(_dbrepository);
    }



    public IActionResult Privacy()
    {
        return View();
    }
    [HttpPost]
    public IActionResult SubmitClaim([FromBody] Ticket? newTicket)

    {
        Console.WriteLine("This is a received request");

        if (newTicket == null)
        {
            Console.WriteLine("The ticket is null");
            return BadRequest("Invalid client request");
        }
        return Created("TicketForm/SubmitClaim", _service.CreateNewTicket(newTicket));
    }
}


