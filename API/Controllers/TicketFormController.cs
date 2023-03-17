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

    public TicketFormController(ILogger<TicketFormController> logger, AccountService service)
    {
        _logger = logger;
        _service = service;
    }



    public IActionResult Privacy()
    {
        return View();
    }
    [HttpPost]
    public IActionResult SubmitClaim([FromBody] NewTicketModel? newClaim)

    {
        Console.WriteLine("This is a received request");

        if (newClaim == null)
        {
            Console.WriteLine("The ticket is null");
            return BadRequest("Invalid client request");
        }
        Ticket newTicket = new Ticket(newClaim.Amount,newClaim.ClientId, newClaim.Description, "" + newClaim.DamagerId, newClaim.DamageDate);
        return Created("TicketForm/SubmitClaim", _service.CreateNewTicket(newTicket));
    }
}


