using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using Microsoft.Extensions.Logging;
using Models;
using Data;
using Services;

namespace API.Controllers;

public class ClientViewTickets : Controller
{
    private readonly ILogger<ClientViewTickets> _logger;
    private readonly AccountService _service;
    private readonly IRepository _dbrepository;

    public ClientViewTickets(ILogger<ClientViewTickets> logger)
    {
        _logger = logger;
        _dbrepository = new DBRepository();
        _service = new AccountService(_dbrepository);
    }

    public IActionResult Privacy()
    {
        return View();
    }
    [HttpGet]
    public IActionResult GetAllClaims([FromBody] int? UserId)
    {
        if (UserId.HasValue)
        {
            return Ok(_service.GetTicketsByUserId(UserId.Value));
        }
        else
        {
            return BadRequest("You must provide an User Id.");
        }

    }

}