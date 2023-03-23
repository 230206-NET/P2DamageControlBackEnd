using API.Controllers;
using API.Models;
using Models;
using Data;
using Services;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests;

public class ClientViewTicketsControllerTests
{
    private readonly ClientViewTicketsController _controller;
    private readonly Mock<IRepository> _mockRepo;
    private readonly Mock<ILogger<ClientViewTicketsController>> _mockIlogger;
    private readonly Mock<AccountService> _mockservice;



    public ClientViewTicketsControllerTests()
    {
        _mockIlogger = new Mock<ILogger<ClientViewTicketsController>>();
        _mockservice = new Mock<AccountService>(_mockRepo);
        _controller = new ClientViewTicketsController(_mockIlogger.Object, _mockservice.Object);
    }

    [Fact]
    public void Privacy_Action_Executes_ReturnsView()
    {
        var result = _controller.Privacy();
        Assert.IsType<ViewResult>(result);
    }
    [Fact]
    public void Client_View_Tickets_Returns_Ok()
    {
        UserRequestModel Id = new UserRequestModel();
        Id.id = 1;
        _mockservice.Setup(p => p.GetTicketsByUserId(Id.id)).Returns(new List<Ticket>{
            new Ticket(1, (decimal) 10.5, 3, new System.DateTime(), new System.DateTime(), "Filler", "1")
        });
        var result = _controller.GetAllClaims(Id);
        Assert.IsType<OkObjectResult>(result);
    }
    [Fact]
    public void Client_View_Tickets_Returns_BadRequest()
    {
        UserRequestModel Id = new UserRequestModel();
        Id.id = 0;
        // _mockservice.Setup(p => p.GetTicketsByUserId(Id.id)).Returns(new List<Ticket>{
        //     new Ticket(1, (decimal) 10.5, 3, new System.DateTime(), new System.DateTime(), "Filler", "1")
        // });
        var result = _controller.GetAllClaims(Id);
        Assert.IsType<BadRequestObjectResult>(result);
    }

}

public class EmployeeViewTicketsControllerTests
{
    public EmployeeViewTicketsController _controller;
    public Mock<IRepository> _mockRepo;
    public readonly Mock<ILogger<EmployeeViewTicketsController>> _mockIlogger;
    public Mock<AccountService> _mockservice;

    public EmployeeViewTicketsControllerTests()
    {
        _mockIlogger = new Mock<ILogger<EmployeeViewTicketsController>>();
        _mockservice = new Mock<AccountService>(_mockRepo);
        _controller = new EmployeeViewTicketsController(_mockIlogger.Object, _mockservice.Object);
    }
    [Fact]
    public void Privacy_Action_Executes_ReturnsView()
    {
        var result = _controller.Privacy();
        Assert.IsType<ViewResult>(result);
    }
    [Fact]
    public void Employee_View_Tickets()
    {
        _mockservice.Setup(p => p.GetAllTickets()).Returns(new List<Ticket>{
            new Ticket(1, (decimal) 10.5, 3, new System.DateTime(), new System.DateTime(), "Filler", "1")
        });
        var result = _controller.GetAllClaims();
        Assert.IsType<OkObjectResult>(result);
    }
    [Fact]
    public void Employee_Get_Pending_Claims()
    {
        _mockservice.Setup(p => p.GetPendingTickets()).Returns(new List<Ticket>{
            new Ticket(1, (decimal) 10.5, 3, new System.DateTime(), new System.DateTime(), "Filler", "1")
        });
        var result = _controller.GetPendingClaims();
        Assert.IsType<OkObjectResult>(result);
    }
    [Fact]
    public void Employee_Update_Ticket_Status()
    {
        TicketStatusChange newTicket = new TicketStatusChange(1, 1, "something", 3, 3);
        _mockservice.Setup(p => p.UpdateTicketStatusWithUserId(newTicket.UserId, newTicket.TicketId, newTicket.Status, newTicket.Justification)).Returns(new Ticket(1, (decimal)10.05, 1, 1, new System.DateTime(), new System.DateTime(), "Filler", "1", "Filler", 2));
        var result = _controller.UpdateTicketStatus(newTicket);
        Assert.IsType<AcceptedResult>(result);
    }

}
public class IndexControllerTests
{
    private readonly IndexController _controller;
    private readonly Mock<ILogger<IndexController>> _mockIlogger;

    public IndexControllerTests()
    {
        _mockIlogger = new Mock<ILogger<IndexController>>();
        _controller = new IndexController(_mockIlogger.Object);
    }
    [Fact]
    public void Privacy_Action_Executes_ReturnsView()
    {
        var result = _controller.Privacy();
        Assert.IsType<ViewResult>(result);
    }
    [Fact]
    public void Index_Action_Executes_ReturnsView()
    {
        var result = _controller.Privacy();
        Assert.IsType<ViewResult>(result);
    }
    // [Fact]
    // public void Error_Action_Executes_ReturnsView()
    // {
    //     var result = _controller.Error();
    //     Assert.IsType<ViewResult>(result);
    // }
}
public class LogInControllerTests
{
    private readonly NewLogInController _controller;
    private readonly Mock<IRepository> _mockRepo;
    private readonly Mock<ILogger<NewLogInController>> _mockIlogger;
    private readonly Mock<AccountService> _mockservice;

    public LogInControllerTests()
    {
        _mockIlogger = new Mock<ILogger<NewLogInController>>();
        _mockservice = new Mock<AccountService>(_mockRepo);
        _controller = new NewLogInController(_mockIlogger.Object, _mockservice.Object);
    }
    [Fact]
    public void Privacy_Action_Executes_ReturnsView()
    {
        var result = _controller.Privacy();
        Assert.IsType<ViewResult>(result);
    }
    [Fact]
    public void ShouldLogInUser()
    {
        LoginModel loginModel = new LoginModel("6OfCrows", "Jordie");
        _mockservice.Setup(x => x.GetUserByUsername(loginModel.Username)).Returns(new User(1, "6OfCrows", "bAAwreVboSXLELCJocDNNg==:SzMMyjkOtEaTik3Ip6nBTih2WLkmnCZAVet1pZK3/Lo=", "Kaz Brekker", "test@test.com", 2));
        var result = _controller.LogIn(loginModel);
        Assert.IsType<OkObjectResult>(result);
    }
    [Fact]
    public void ShouldReturnBadRequest()
    {
        LoginModel loginModel = new LoginModel("6OfCrows", "Jordie");
        _mockservice.Setup(x => x.GetUserByUsername(loginModel.Username)).Returns((User)null);
        var result = _controller.LogIn(loginModel);
        Assert.IsType<BadRequestObjectResult>(result);
    }
    [Fact]
    public void shouldReturnUnauthorized()
    {
        LoginModel loginModel = new LoginModel("6OfCrows", "Jordie");
        _mockservice.Setup(x => x.GetUserByUsername(loginModel.Username)).Returns(new User(1, "6OfCrows", "bAAwreVboSXLELCJocDNNg==:SzMMyjkOtEaTik3Ip6nBTih2WLkmnCZAVet1pZK3/LP=", "Kaz Brekker", "test@test.com", 2));
        var result = _controller.LogIn(loginModel);
        Assert.IsType<UnauthorizedResult>(result);
    }
}
public class RegisterControllerTests
{
    private readonly RegisterController _controller;
    private readonly Mock<IRepository> _mockRepo;
    private readonly Mock<ILogger<RegisterController>> _mockIlogger;
    private readonly Mock<AccountService> _mockservice;

    public RegisterControllerTests()
    {
        _mockIlogger = new Mock<ILogger<RegisterController>>();
        _mockservice = new Mock<AccountService>(_mockRepo);
        _controller = new RegisterController(_mockIlogger.Object, _mockservice.Object);
    }
    [Fact]
    public void Privacy_Action_Executes_ReturnsView()
    {
        var result = _controller.Privacy();
        Assert.IsType<ViewResult>(result);
    }
    [Fact]
    public void RegisterShouldReturnCreated()
    {
        User newUser = new User(1, "test", "testpw", "Test User", "test@test.com", 0);
        _mockservice.Setup(x => x.CreateNewUser(newUser)).Returns(newUser);
        var result = _controller.Register(newUser);
        Assert.IsType<CreatedResult>(result);
    }
    public void RegisterShouldReturnBadRequest()
    {
        var result = _controller.Register(null);
        Assert.IsType<BadRequestObjectResult>(result);
    }
}
public class TicketFormControllerTests
{
    private readonly TicketFormController _controller;
    private readonly Mock<IRepository> _mockRepo;
    private readonly Mock<ILogger<TicketFormController>> _mockIlogger;
    private readonly Mock<AccountService> _mockservice;

    public TicketFormControllerTests()
    {
        _mockIlogger = new Mock<ILogger<TicketFormController>>();
        _mockservice = new Mock<AccountService>(_mockRepo);
        _controller = new TicketFormController(_mockIlogger.Object, _mockservice.Object);
    }
    [Fact]
    public void Privacy_Action_Executes_ReturnsView()
    {
        var result = _controller.Privacy();
        Assert.IsType<ViewResult>(result);
    }
    [Fact]
    public void SubmitClaimShouldReturnBadRequest()
    {
        var result = _controller.SubmitClaim(null);
        Assert.IsType<BadRequestObjectResult>(result);
    }
    [Fact]
    public void SubmitClaimShouldReturnCreated()
    {
        NewTicketModel ticektModel = new NewTicketModel(1, (decimal)10.5, "02/02/2002", 105673, "Filler");
        Ticket newTicket = new Ticket(ticektModel.Amount, ticektModel.ClientId, ticektModel.Description, "" + ticektModel.DamagerId, ticektModel.DamageDate);
        _mockservice.Setup(p => p.CreateNewTicket(newTicket)).Returns(newTicket);
        var result = _controller.SubmitClaim(ticektModel);
        Assert.IsType<CreatedResult>(result);
    }


}

public class EmployeeAdminControllerTests
{
    private readonly EmployeeAdminController _controller;
    private readonly Mock<IRepository> _mockRepo;
    private readonly Mock<ILogger<EmployeeAdminController>> _mockIlogger;
    private readonly Mock<AccountService> _mockservice;

    public EmployeeAdminControllerTests()
    {
        _mockIlogger = new Mock<ILogger<EmployeeAdminController>>();
        _mockservice = new Mock<AccountService>(_mockRepo);
        _controller = new EmployeeAdminController(_mockIlogger.Object, _mockservice.Object);
    }
    [Fact]
    public void Privacy_Action_Executes_ReturnsView()
    {
        var result = _controller.Privacy();
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void Admin_UpdateUserAccessLevel_Returns_Ok()
    {
        EmployeeLevelChange ELC = new EmployeeLevelChange(1, 2, 3);
        // ELC.AdminId = 1;
        // ELC.UserId = 2;
        // ELC.AccessLevel = 3;
        User newUser = new User(1, "test", "testpw", "Test User", "test@test.com", 0);
        // UserRequestModel Id = new UserRequestModel();
        // Id.id = 1;
        _mockservice.Setup(p => p.UpdateUserAccessLevelWithUserId(ELC.AdminId, ELC.UserId, ELC.AccessLevel)).Returns(newUser);
        var result = _controller.UpdateUserAccessLevel(ELC);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void Admin_Get_Users()
    {
        _mockservice.Setup(p => p.GetAllUsers()).Returns(new List<User>{
            new User(1, "name", "password", "anothername", "email", 0)
        });
        var result = _controller.GetAllUsers();
        Assert.IsType<OkObjectResult>(result);
    }
    [Fact]
    public void Admin_UpdateUserAccessLevel_Returns_BadRequest()
    {
        var result = _controller.UpdateUserAccessLevel(null);
        Assert.IsType<BadRequestObjectResult>(result);
    }
}