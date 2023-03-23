using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using Microsoft.Extensions.Logging;
using Models;
using Data;
using Services;

namespace API.Controllers;

public class EmployeeViewTicketsController : Controller
{
    private readonly ILogger<EmployeeViewTicketsController> _logger;
    private readonly AccountService _service;

    public EmployeeViewTicketsController(ILogger<EmployeeViewTicketsController> logger, AccountService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAllClaims()
    {
        return Ok(_service.GetAllTickets());
    }
    [HttpGet]
    public IActionResult GetPendingClaims()
    {
        return Ok(_service.GetPendingTickets());
    }

    [HttpPut]
    public IActionResult UpdateTicketStatus([FromBody] TicketStatusChange ticketstatus)
    {
        return Accepted("EmployeeViewTickets/UpdateTicketStatus", _service.UpdateTicketStatusWithUserId(ticketstatus.UserId, ticketstatus.TicketId, ticketstatus.Status, ticketstatus.Justification));
    }
}
