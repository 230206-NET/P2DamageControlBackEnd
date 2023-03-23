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
}

public class EmployeeViewTicketsControllerTests
{
    private readonly EmployeeViewTicketsController _controller;
    private readonly Mock<IRepository> _mockRepo;
    private readonly Mock<ILogger<EmployeeViewTicketsController>> _mockIlogger;
    private readonly Mock<AccountService> _mockservice;

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
}
