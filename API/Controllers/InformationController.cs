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
    private readonly ILogger<InformationController> _logger;
    private readonly AccountService _service;

    public InformationController(ILogger<InformationController> logger, AccountService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpPut]
    public IActionResult ChangeInfo([FromBody] User? modifiedUser)

    {
        Console.WriteLine("This is a received request");

        if (modifiedUser != null)
        {
            Console.WriteLine(modifiedUser.Email);
            if (modifiedUser.Password != "N/A")
            {
                modifiedUser.Password = PasswordService.HashAndSaltPassword(modifiedUser.Password);
            }
            return Accepted("Information/ChangeInfo", _service.UpdateUserInfo(modifiedUser));
        }
        else
        {
            Console.WriteLine("The User was not received");
            return BadRequest("Invalid client request");

        }
    }
    [HttpPost]
    public IActionResult Info([FromBody] UserRequestModel user)

    {
        Console.WriteLine("This is a received request");

        if (user == null)
        {
            return BadRequest("Invalid client request");
        }
        User? specifiedUser = _service.GetUserByUserId(user.id);
        if (specifiedUser != null)
        {
            specifiedUser.Password = "N/A";
            return Ok(specifiedUser);
        }
        else
        {
            return BadRequest("Invalid user information. Please log in again");
        }
    }
}
