using Models;
using Services;
using Data;
using System.Data.SqlClient;
using Moq;

namespace Tests;


public class AccountServiceTests
{
    public SqlException MakeSqlException()
    {
        SqlException exception = null;
        try
        {
            SqlConnection conn = new SqlConnection(@"Data Source=.;Database=GUARANTEED_TO_FAIL;Connection Timeout=1");
            conn.Open();
        }
        catch (SqlException ex)
        {
            exception = ex;
        }
        return (exception);
    }

    [Fact]
    public void GetAllUsersShouldReturnUserList()
    {
        var moqRepo = new Mock<IRepository>();
        moqRepo.Setup(repo => repo.GetAllUsers()).Returns(
            new List<User>
            {
                new User(1, "mockUser", "mockPass", "mockName", "mockEmail@Email.com", 0)
            }
        );

        AccountService accountService = new AccountService(moqRepo.Object);
        List<User> allUsers = accountService.GetAllUsers();

        Assert.NotNull(allUsers);
        Assert.Equal(1, allUsers[0].Id);
    }

    [Fact]
    public void GetAllUsersShouldThrowSqlException()
    {
        var moqRepo = new Mock<IRepository>();
        moqRepo.Setup(repo => repo.GetAllUsers()).Throws(
            MakeSqlException()
        );

        AccountService accountService = new AccountService(moqRepo.Object);
        Assert.Throws<SqlException>(() => accountService.GetAllUsers());
    }

    [Fact]
    public void UpdateUserInfoShouldChangeInfo()
    {
        var moqRepo = new Mock<IRepository>();
        User user = new User(1, "mockUser", "mockPass", "mockName", "mockEmail@Email.com", 0);
        moqRepo.Setup(repo => repo.UpdateUserInfo(user));

        AccountService accountService = new AccountService(moqRepo.Object);
        User userReturned = accountService.UpdateUserInfo(user);

        Assert.NotNull(userReturned);
    }

    [Fact]
    public void UpdateUserInfoShouldThrowSqlException()
    {
        var moqRepo = new Mock<IRepository>();
        User user = new User(1, "mockUser", "mockPass", "mockName", "mockEmail@Email.com", 0);
        moqRepo.Setup(repo => repo.UpdateUserInfo(user)).Throws(
            MakeSqlException()
        );

        AccountService accountService = new AccountService(moqRepo.Object);
        Assert.Throws<SqlException>(() => accountService.UpdateUserInfo(user));
    }
    [Fact]
    public void GetUserByUsernameShouldReturnUser()
    {
        var moqRepo = new Mock<IRepository>();
        string username = "Test";
        moqRepo.Setup(repo => repo.GetUserByUsername(username)).Returns(
                new User(1, "Test", "mockPass", "mockName", "mockEmail@Email.com", 0)
        );

        AccountService accountService = new AccountService(moqRepo.Object);
        User user = accountService.GetUserByUsername(username);

        Assert.NotNull(user);
        Assert.Equal("Test", user.Username);
    }

    [Fact]
    public void GetUserByUsernameShouldThrowSqlException()
    {
        var moqRepo = new Mock<IRepository>();
        string username = "Test";
        moqRepo.Setup(repo => repo.GetUserByUsername(username)).Throws(
            MakeSqlException()
        );

        AccountService accountService = new AccountService(moqRepo.Object);
        Assert.Throws<SqlException>(() => accountService.GetUserByUsername(username));
    }

    [Fact]
    public void GetUserByUserIdShouldReturnUser()
    {
        var moqRepo = new Mock<IRepository>();
        int id = 1;
        moqRepo.Setup(repo => repo.GetUserByUserId(id)).Returns(
                new User(1, "mockUser", "mockPass", "mockName", "mockEmail@Email.com", 0)
        );

        AccountService accountService = new AccountService(moqRepo.Object);
        User user = accountService.GetUserByUserId(id);

        Assert.NotNull(user);
        Assert.Equal(1, user.Id);
    }

    [Fact]
    public void GetUserByUserIdShouldThrowSqlException()
    {
        var moqRepo = new Mock<IRepository>();
        int id = 1;
        moqRepo.Setup(repo => repo.GetUserByUserId(id)).Throws(
            MakeSqlException()
        );

        AccountService accountService = new AccountService(moqRepo.Object);
        Assert.Throws<SqlException>(() => accountService.GetUserByUserId(id));
    }

    [Fact]
    public void UsernameExistsShouldReturnTrue()
    {
        var moqRepo = new Mock<IRepository>();
        string username = "Test";
        moqRepo.Setup(repo => repo.GetUserByUsername(username)).Returns(
            new User(1, "Test", "mockPass", "mockName", "mockEmail@Email.com", 0)
        );

        AccountService accountService = new AccountService(moqRepo.Object);
        Boolean exists = accountService.UsernameExists(username);

        Assert.Equal(true, exists);
    }
    [Fact]
    public void UsernameExistsShouldReturnFalse()
    {
        var moqRepo = new Mock<IRepository>();
        string username = "Test";
        moqRepo.Setup(repo => repo.GetUserByUsername(username));

        AccountService accountService = new AccountService(moqRepo.Object);
        Boolean exists = accountService.UsernameExists(username);

        Assert.Equal(false, exists);
    }

    [Fact]
    public void UsernameExistsShouldThrowSqlException()
    {
        var moqRepo = new Mock<IRepository>();
        string username = "Test";
        moqRepo.Setup(repo => repo.GetUserByUsername(username)).Throws(
            MakeSqlException()
        );

        AccountService accountService = new AccountService(moqRepo.Object);
        Assert.Throws<SqlException>(() => accountService.UsernameExists(username));
    }
    [Fact]
    public void CreateNewUserShouldReturnUser()
    {
        var moqRepo = new Mock<IRepository>();
        User newUser = new User(1, "mockUser", "mockPass", "mockName", "mockEmail@Email.com", 0);
        moqRepo.Setup(repo => repo.CreateNewUser(newUser));

        AccountService accountService = new AccountService(moqRepo.Object);
        User UserReturned = accountService.CreateNewUser(newUser);

        Assert.NotNull(UserReturned);
        Assert.Equal(1, UserReturned.Id);
    }

    [Fact]
    public void CreateNewUserShouldThrowSqlException()
    {
        var moqRepo = new Mock<IRepository>();
        User newUser = new User(1, "mockUser", "mockPass", "mockName", "mockEmail@Email.com", 0);
        moqRepo.Setup(repo => repo.CreateNewUser(newUser)).Throws(
            MakeSqlException()
        );

        AccountService accountService = new AccountService(moqRepo.Object);
        Assert.Throws<SqlException>(() => accountService.CreateNewUser(newUser));
    }
    [Fact]
    public void GetAllTicketsShouldReturnTicketList()
    {
        var moqRepo = new Mock<IRepository>();
        moqRepo.Setup(repo => repo.GetAllTickets()).Returns(
            new List<Ticket>
            {
                new Ticket(1, 500.0m, 1, DateTime.Now, DateTime.Now, "Description", "DamagerId")
            }
        );

        AccountService accountService = new AccountService(moqRepo.Object);
        List<Ticket> allTickets = accountService.GetAllTickets();

        Assert.NotNull(allTickets);
        Assert.Equal(1, allTickets[0].Id);
    }

    [Fact]
    public void GetAllTicketsShouldThrowSqlException()
    {
        var moqRepo = new Mock<IRepository>();
        moqRepo.Setup(repo => repo.GetAllTickets()).Throws(
            MakeSqlException()
        );

        AccountService accountService = new AccountService(moqRepo.Object);
        Assert.Throws<SqlException>(() => accountService.GetAllTickets());
    }
    [Fact]
    public void GetTicketByIdShouldReturnTicket()
    {
        var moqRepo = new Mock<IRepository>();
        int id = 1;
        moqRepo.Setup(repo => repo.GetAllTickets()).Returns(
            new List<Ticket>{
                new Ticket(1, 500.0m, 1, DateTime.Now, DateTime.Now, "Description", "DamagerId")
            }
        );

        AccountService accountService = new AccountService(moqRepo.Object);
        Ticket ticket = accountService.GetTicketById(id);

        Assert.NotNull(ticket);
        Assert.Equal(1, ticket.Id);
    }

    [Fact]
    public void GetTicketByIdShouldThrowSqlException()
    {
        var moqRepo = new Mock<IRepository>();
        int id = 1;
        moqRepo.Setup(repo => repo.GetAllTickets()).Throws(
            MakeSqlException()
        );

        AccountService accountService = new AccountService(moqRepo.Object);
        Assert.Throws<SqlException>(() => accountService.GetTicketById(id));
    }
    [Fact]
    public void GetTicketByIdShouldThrowInvalidOperationException()
    {
        var moqRepo = new Mock<IRepository>();
        int id = 1;
        moqRepo.Setup(repo => repo.GetAllTickets()).Throws(
            new InvalidOperationException()
        );

        AccountService accountService = new AccountService(moqRepo.Object);
        Assert.Throws<InvalidOperationException>(() => accountService.GetTicketById(id));
    }

    [Fact]
    public void GetTicketsByUserIdShouldReturnTicketList()
    {
        var moqRepo = new Mock<IRepository>();
        int id = 1;
        moqRepo.Setup(repo => repo.GetTicketsByUserId(id)).Returns(
            new List<Ticket>{
                new Ticket(1, 500.0m, 1, DateTime.Now, DateTime.Now, "Description", "DamagerId")
            }
        );

        AccountService accountService = new AccountService(moqRepo.Object);
        List<Ticket> allTickets = accountService.GetTicketsByUserId(id);

        Assert.NotNull(allTickets);
        Assert.Equal(1, allTickets[0].ClientId);
    }

    [Fact]
    public void GetTicketsByUserIdShouldThrowSqlException()
    {
        var moqRepo = new Mock<IRepository>();
        int id = 1;
        moqRepo.Setup(repo => repo.GetTicketsByUserId(id)).Throws(
            MakeSqlException()
        );

        AccountService accountService = new AccountService(moqRepo.Object);
        Assert.Throws<SqlException>(() => accountService.GetTicketsByUserId(id));
    }
    [Fact]
    public void GetPendingTicketsShouldReturnTicketList()
    {
        var moqRepo = new Mock<IRepository>();
        moqRepo.Setup(repo => repo.GetPendingTickets()).Returns(
            new List<Ticket>{
                new Ticket(1, 500.0m, 1, DateTime.Now, DateTime.Now, "Description", "DamagerId")
            }
        );

        AccountService accountService = new AccountService(moqRepo.Object);
        List<Ticket> allTickets = accountService.GetPendingTickets();

        Assert.NotNull(allTickets);
    }

    [Fact]
    public void GetPendingTicketsShouldThrowSqlException()
    {
        var moqRepo = new Mock<IRepository>();
        moqRepo.Setup(repo => repo.GetPendingTickets()).Throws(
            MakeSqlException()
        );

        AccountService accountService = new AccountService(moqRepo.Object);
        Assert.Throws<SqlException>(() => accountService.GetPendingTickets());
    }

    [Fact]
    public void CreateNewTicketShouldReturnTicket()
    {
        var moqRepo = new Mock<IRepository>();
        Ticket newTicket = new Ticket(1, 500.0m, 1, DateTime.Now, DateTime.Now, "Description", "DamagerId");
        moqRepo.Setup(repo => repo.CreateNewTicket(newTicket));

        AccountService accountService = new AccountService(moqRepo.Object);
        Ticket TicketReturned = accountService.CreateNewTicket(newTicket);

        Assert.NotNull(TicketReturned);
        Assert.Equal(1, TicketReturned.Id);
    }

    [Fact]
    public void CreateNewTicketShouldThrowSqlException()
    {
        var moqRepo = new Mock<IRepository>();
        Ticket newTicket = new Ticket(1, 500.0m, 1, DateTime.Now, DateTime.Now, "Description", "DamagerId");
        moqRepo.Setup(repo => repo.CreateNewTicket(newTicket)).Throws(
            MakeSqlException()
        );

        AccountService accountService = new AccountService(moqRepo.Object);
        Assert.Throws<SqlException>(() => accountService.CreateNewTicket(newTicket));
    }
    [Fact]
    public void UpdateTicketStatusShouldUpdateTicket()
    {
        var moqRepo = new Mock<IRepository>();
        moqRepo.Setup(repo => repo.UpdateTicketStatus(1, 1, "Test", 1));
        moqRepo.Setup(repo => repo.GetAllTickets()).Returns(
            new List<Ticket>{
                new Ticket(1, 500.0m, 1, DateTime.Now, DateTime.Now, "Description", "DamagerId")
            }
        );

        AccountService accountService = new AccountService(moqRepo.Object);
        Ticket ticketReturned = accountService.UpdateTicketStatus(1, 1, "Test", 1);

        Assert.NotNull(ticketReturned);
    }

    [Fact]
    public void UpdateTicketStatusShouldThrowSqlException()
    {
        var moqRepo = new Mock<IRepository>();
        moqRepo.Setup(repo => repo.UpdateTicketStatus(1, 1, "Test", 1)).Throws(
            MakeSqlException()
        );

        AccountService accountService = new AccountService(moqRepo.Object);
        Assert.Throws<SqlException>(() => accountService.UpdateTicketStatus(1, 1, "Test", 1));
    }
    [Fact]
    public void UpdateTicketStatusWithUserIdShouldUpdateTicket()
    {
        var moqRepo = new Mock<IRepository>();
        int id = 1;
        moqRepo.Setup(repo => repo.UpdateTicketStatus(1, 1, "Test", 1));
        moqRepo.Setup(repo => repo.GetAllTickets()).Returns(
            new List<Ticket>{
                new Ticket(1, 500.0m, 1, 2, DateTime.Now, DateTime.Now, "Description", "DamagerId", "justified", 0)
            }
        );
        moqRepo.Setup(repo => repo.GetUserByUserId(id)).Returns(
            new User(1, "mockUser", "mockPass", "mockName", "mockEmail@Email.com", 3)
        );

        AccountService accountService = new AccountService(moqRepo.Object);
        Ticket ticketReturned = accountService.UpdateTicketStatusWithUserId(1, 1, 1, "Test");

        Assert.NotNull(ticketReturned);
    }

    [Fact]
    public void UpdateTicketStatusWithUserIdShouldThrowEarlySqlException()
    {
        var moqRepo = new Mock<IRepository>();
        int id = 1;
        moqRepo.Setup(repo => repo.UpdateTicketStatus(1, 1, "Test", 1)).Throws(
            MakeSqlException()
        );
        moqRepo.Setup(repo => repo.GetAllTickets()).Returns(
            new List<Ticket>{
                new Ticket(1, 500.0m, 1, 2, DateTime.Now, DateTime.Now, "Description", "DamagerId", "justified", 0)
            }
        );
        moqRepo.Setup(repo => repo.GetUserByUserId(id)).Throws(
            MakeSqlException()
        );

        AccountService accountService = new AccountService(moqRepo.Object);
        Assert.Throws<SqlException>(() => accountService.UpdateTicketStatusWithUserId(1, 1, 1, "Test"));
    }

    [Fact]
    public void UpdateTicketStatusWithUserIdShouldThrowAnotherSqlException()
    {
        var moqRepo = new Mock<IRepository>();
        int id = 1;
        moqRepo.Setup(repo => repo.UpdateTicketStatus(1, 1, "Test", 1)).Throws(
            MakeSqlException()
        );
        moqRepo.Setup(repo => repo.GetAllTickets()).Returns(
            new List<Ticket>{
                new Ticket(1, 500.0m, 1, 2, DateTime.Now, DateTime.Now, "Description", "DamagerId", "justified", 0)
            }
        );
        moqRepo.Setup(repo => repo.GetUserByUserId(id)).Returns(
            new User(1, "mockUser", "mockPass", "mockName", "mockEmail@Email.com", 3)
        );

        AccountService accountService = new AccountService(moqRepo.Object);
        Assert.Throws<SqlException>(() => accountService.UpdateTicketStatusWithUserId(1, 1, 1, "Test"));
    }
    [Fact]
    public void UpdateUserAccessLevelShouldChangeInfo()
    {
        var moqRepo = new Mock<IRepository>();
        moqRepo.Setup(repo => repo.UpdateUserAccessLevel(1, 3));

        AccountService accountService = new AccountService(moqRepo.Object);
        accountService.UpdateUserAccessLevel(1, 3);

    }

    [Fact]
    public void UpdateUserAccessLevelShouldThrowSqlException()
    {
        var moqRepo = new Mock<IRepository>();
        moqRepo.Setup(repo => repo.UpdateUserAccessLevel(1, 3)).Throws(
            MakeSqlException()
        );

        AccountService accountService = new AccountService(moqRepo.Object);
        Assert.Throws<SqlException>(() => accountService.UpdateUserAccessLevel(1, 3));
    }
    [Fact]
    public void DeclineAllPendingTicketsForUserIdShouldChangeInfo()
    {
        var moqRepo = new Mock<IRepository>();
        moqRepo.Setup(repo => repo.DeclineAllPendingTicketsForUserId(1));

        AccountService accountService = new AccountService(moqRepo.Object);
        accountService.DeclineAllPendingTicketsForUserId(1);

    }

    [Fact]
    public void DeclineAllPendingTicketsForUserIdShouldThrowSqlException()
    {
        var moqRepo = new Mock<IRepository>();
        moqRepo.Setup(repo => repo.DeclineAllPendingTicketsForUserId(1)).Throws(
            MakeSqlException()
        );

        AccountService accountService = new AccountService(moqRepo.Object);
        Assert.Throws<SqlException>(() => accountService.DeclineAllPendingTicketsForUserId(1));
    }

}