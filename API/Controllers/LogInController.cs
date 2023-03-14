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

namespace API.Controllers;

public class NewLogInController : Controller
{
    private readonly ILogger<NewLogInController> _logger;


    public NewLogInController(ILogger<NewLogInController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View("NewLogin");
    }

    public IActionResult Privacy()
    {
        return View();
    }
    [HttpPost]
    public IActionResult LogIn([FromBody] LoginCredentials credentials)
    {
        User? user = new DBRepository().GetUserByUsername(credentials.Username); //gonna switch to a struct... ill talk to them about it and see

        if (User == null)
        {
            return BadRequest("Invalid client request");
        }
        if (Services.PasswordService.VerifyPassword(credentials.Password, user.Password)) //do i use this method or the Login method since the password is hashed?
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.GivenName, user.FullName),
                new Claim(ClaimTypes.Role, user.AccessLevel.ToString())
            };
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JustForTEsTINGGGG@THeMomENT4536435"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: "https://localhost:7026",
                audience: "https://localhost:7026",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return Ok(new AuthenticatedResponse { Token = tokenString });
        }
        return Unauthorized();
    }
}


