using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using Microsoft.Extensions.Logging;
using Models;
using Data;
using Services;

namespace API.Controllers;

public class EmployeeViewTickets : Controller
{
    private readonly ILogger<EmployeeViewTickets> _logger;
    private readonly AccountService _service;
    private readonly IRepository _dbrepository;

    public EmployeeViewTickets(ILogger<EmployeeViewTickets> logger)
    {
        _logger = logger;
        _dbrepository = new DBRepository();
        _service = new AccountService(_dbrepository);
    }

    public IActionResult Index()
    {
        ViewBag.CurrentCategory = GetAllClaims();
        return View("EmployeeViewTickets");
    }

    public IActionResult Privacy()
    {
        return View();
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
        return Accepted("EmployeViewTickets/UpdateTicketStatus", _service.UpdateTicketStatus(ticketstatus.UserId, ticketstatus.Status, ticketstatus.Justification, ticketstatus.UserId));
    }
}
