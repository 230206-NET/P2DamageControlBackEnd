using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using Microsoft.Extensions.Logging;
using Models;
using Data;
using System.Text.Json;
using Services;

namespace API.Controllers;

public class RegisterController : Controller
{
    private readonly ILogger<RegisterController> _logger;
    private readonly AccountService _service;


    public RegisterController(ILogger<RegisterController> logger, AccountService service)
    {
        _logger = logger;
        _service = service;
    }



    public IActionResult Privacy()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Register([FromBody] User? newUser)
    {
        if (newUser == null)
        {
            return BadRequest("Invalid client request");
        }
        return Created("Register/Register", _service.CreateNewUser(newUser));
    }
}


