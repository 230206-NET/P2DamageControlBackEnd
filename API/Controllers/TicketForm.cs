using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Models;
using Data;

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
    public IActionResult SubmitClaim([FromBody] JsonElement claimData)

    {
        try
        {
            var data = JsonSerializer.Deserialize<Body>(claimData.GetRawText());
            Ticket newTicket = new Ticket(data.Amount, data.Description, data.Damager);

            Console.WriteLine(data.Amount);
            //Ticket newTicket = new Ticket(data.Amount, 1, data.dateOfDamage, description, damager);
            new DBRepository().CreateNewTicket(newTicket);
            return Json(new { success = true });

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return Json(new { success = false });

        }
    }
}


