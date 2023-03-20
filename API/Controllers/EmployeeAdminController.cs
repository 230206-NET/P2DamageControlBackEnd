using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using Microsoft.Extensions.Logging;
using Models;
using Data;
using Services;

namespace API.Controllers;

public class EmployeeAdminController : Controller
{
    private readonly ILogger<EmployeeAdminController> _logger;
    private readonly AccountService _service;

    public EmployeeAdminController(ILogger<EmployeeAdminController> logger, AccountService service)
    {
        _logger = logger;
        _service = service;
    }

    public IActionResult Privacy()
    {
        return View();
    }
    [HttpPut]
    public IActionResult UpdateUserAccessLevel([FromBody] EmployeeLevelChange ELC)
    {
        if (ELC != null)
        {
            return Ok(_service.UpdateUserAccessLevelWithUserId(ELC.AdminId, ELC.UserId, ELC.AccessLevel));
        }
        else
        {
            return BadRequest("You must provide an Admin Id, User Id, and Access level.");
        }

    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        return Ok(_service.GetAllUsers());
    }

}