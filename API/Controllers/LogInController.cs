using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using Microsoft.Extensions.Logging;
using Models;
using Data;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Services;

namespace API.Controllers;

public class NewLogInController : Controller
{
    private readonly ILogger<NewLogInController> _logger;
    private readonly AccountService _service;


    public NewLogInController(ILogger<NewLogInController> logger,  AccountService service)
    {
        _logger = logger;
        _service = service;
    }

    /*public IActionResult Index()
    {
        return View("NewLogin");
    }*/

    public IActionResult Privacy()
    {
        return View();
    }
    [HttpPost]
    public IActionResult LogIn([FromBody] LoginModel credentials)
    {
        Console.WriteLine(credentials);
        User? user = _service.GetUserByUsername(credentials.Username);

        if (user == null)
        {
            Console.WriteLine("Something went wrong");
            return BadRequest("Invalid client request");
        }
        if (PasswordService.VerifyPassword(credentials.Password, user.Password))
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.SerialNumber, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.GivenName, user.FullName),
                new Claim(ClaimTypes.Role, user.AccessLevel.ToString())
            };
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JustForTEsTINGGGG@THeMomENT4536435"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:5025",
                audience: "http://localhost:5025",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return Ok(new AuthenticatedResponse { Token = tokenString });
        }
        Console.WriteLine("Credentials didn't match");
        return Unauthorized();
    }
}


