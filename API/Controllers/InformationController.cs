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

    [HttpPost]
    public IActionResult ChangeInfo([FromBody] User? modifiedUser)

    {
        Console.WriteLine("This is a received request");

        if (modifiedUser != null)
        {
            _dbrepository.UpdateUserInfo(modifiedUser);
            return Ok();
        }
        else{
            Console.WriteLine("The User was not received");
            return BadRequest("Invalid client request");

        }
    }
    [HttpGet]
    public IActionResult Info([FromBody] UserRequestModel user)

    {
        Console.WriteLine("This is a received request");

        if (user == null)
        {
            return BadRequest("Invalid client request");
        }
        User? specifiedUser = _dbrepository.GetUserByUserId(user.Id);
        if (specifiedUser != null){
        specifiedUser.Password = "N/A";
        return Ok(user);
        }
        else{
            return BadRequest("Invalid user information. Please log in again");
        }
    }
}
