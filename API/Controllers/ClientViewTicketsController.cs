using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using Microsoft.Extensions.Logging;
using Models;
using Data;
using Services;

namespace API.Controllers;

public class ClientViewTicketsController : Controller
{
    private readonly ILogger<ClientViewTicketsController> _logger;
    private readonly AccountService _service;

    public ClientViewTicketsController(ILogger<ClientViewTicketsController> logger,  AccountService service)
    {
        _logger = logger;
        _service = service;
    }

    public IActionResult Privacy()
    {
        return View();
    }
    [HttpPut]
    public IActionResult GetAllClaims([FromBody] UserRequestModel Id)
    {
        if (Id.id != 0)
        {
            return Ok(_service.GetTicketsByUserId(Id.id));
        }
        else
        {
            return BadRequest("You must provide an User Id.");
        }

    }

}