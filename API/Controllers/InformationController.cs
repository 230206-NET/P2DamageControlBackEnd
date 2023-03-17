using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Models;
using Data;
using Services;

namespace API.Controllers;

public class InformationController : Controller
{
    private readonly ILogger<TicketFormController> _logger;
    private readonly AccountService _service;
    private readonly IRepository _dbrepository;

    public InformationController(ILogger<TicketFormController> logger)
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
