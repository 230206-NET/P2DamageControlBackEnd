using Models;
using API.Models;

namespace Tests;

public class AuthenticatedResponseTests
{
    [Fact]
    public void AuthenticatedResponseShouldCreate()
    {
        AuthenticatedResponse ar = new();
        Assert.NotNull(ar);
    }

    [Fact]
    public void ShouldSetToken()
    {
        AuthenticatedResponse ar = new();
        ar.Token = "adadsjkdnwaijdhosai";
        Assert.Equal("adadsjkdnwaijdhosai", ar.Token);
    }
}

public class ErrorViewModelTests
{
    [Fact]
    public void ErrorViewModelShouldCreate()
    {
        ErrorViewModel evm = new();
        Assert.NotNull(evm);
    }

    [Fact]
    public void ErrorViewModelShouldNotShowRequestIdOnEmptyString()
    {
        ErrorViewModel evm = new();
        evm.RequestId = "";
        Assert.Equal(false, evm.ShowRequestId);
    }
}

public class NewTicketsModelTests
{
    [Fact]
    public void NewTicketModelShouldCreate()
    {
        NewTicketModel ntm = new();
        Assert.NotNull(ntm);
        Assert.NotNull(ntm.ClientId);
        Assert.NotNull(ntm.Amount);
        Assert.NotNull(ntm.DamagerId);
    }

    [Fact]
    public void ShouldSetValidDescription()
    {
        NewTicketModel ntm = new();
        ntm.Description = "I am here";
        Assert.Equal("I am here", ntm.Description);
    }

    [Fact]
    public void ShouldSetValidDamagerDate()
    {
        NewTicketModel ntm = new();
        ntm.DamageDate = "3/21/2023";
        Assert.Equal("3/21/2023", ntm.DamageDate);
    }
}

public class TicketTests
{
    [Fact]
    public void TicketShouldCreate()
    {
        Ticket t = new();
        Assert.NotNull(t);
        Assert.NotNull(t.Id);
        Assert.NotNull(t.Amount);
        Assert.NotNull(t.ClientId);
        Assert.NotNull(t.EmployeeId);
        Assert.NotNull(t.SubmissionDate);
        Assert.NotNull(t.DamageDate);
        Assert.NotNull(t.TicketJustification);
        Assert.NotNull(t.TicketStatus);
    }

    [Fact]
    public void ShouldSetValidDescription()
    {
        Ticket t = new();
        t.Description = "I am here";
        Assert.Equal("I am here", t.Description);
    }

    [Fact]
    public void ShouldSetValidDamagerId()
    {
        Ticket t = new();
        t.DamagerId = "1";
        Assert.Equal("1", t.DamagerId);
    }

    [Fact]
    public void TicketConstructorOneShouldCreate()
    {
        Ticket t = new Ticket(1, 2, 3, DateTime.Today, "hello", "100");

        Assert.NotNull(t);
        Assert.NotNull(t.Id);
        Assert.NotNull(t.Amount);
        Assert.NotNull(t.ClientId);
        Assert.NotNull(t.DamageDate);
        Assert.NotNull(t.Description);
        Assert.NotNull(t.DamagerId);
    }
    [Fact]
    public void TicketConstructorTwoShouldCreate()
    {
        Ticket t = new Ticket(1, 2, 3, DateTime.Today, DateTime.Today, "hello", "100");

        Assert.NotNull(t);
        Assert.NotNull(t.Id);
        Assert.NotNull(t.Amount);
        Assert.NotNull(t.ClientId);
        Assert.NotNull(t.SubmissionDate);
        Assert.NotNull(t.DamageDate);
        Assert.NotNull(t.Description);
        Assert.NotNull(t.DamagerId);
    }
    [Fact]
    public void TicketConstructorThreeShouldCreate()
    {
        Ticket t = new Ticket(1, 2, 3, 4, DateTime.Today, DateTime.Today, "hello", "100", "Justified", 2);

        Assert.NotNull(t);
        Assert.NotNull(t.Id);
        Assert.NotNull(t.Amount);
        Assert.NotNull(t.ClientId);
        Assert.NotNull(t.EmployeeId);
        Assert.NotNull(t.SubmissionDate);
        Assert.NotNull(t.DamageDate);
        Assert.NotNull(t.Description);
        Assert.NotNull(t.DamagerId);
        Assert.NotNull(t.TicketJustification);
        Assert.NotNull(t.TicketStatus);
    }
    [Fact]
    public void TicketConstructorFourShouldCreate()
    {
        Ticket t = new Ticket(1, 2, "hello", "100", "12/25/2015 10:30:00 AM");

        Assert.NotNull(t);
        Assert.NotNull(t.Amount);
        Assert.NotNull(t.ClientId);
        Assert.NotNull(t.SubmissionDate);
        Assert.NotNull(t.Description);
        Assert.NotNull(t.DamagerId);
        Assert.NotNull(t.DamageDate);
    }
    [Fact]
    public void TicketConstructorFiveShouldCreate()
    {
        Ticket t = new Ticket();

        Assert.NotNull(t);
        Assert.Equal(DateTime.Today, t.SubmissionDate);
    }
}
public class UserTests
{
    [Fact]
    public void UserShouldCreate()
    {
        User u = new();

        Assert.NotNull(u);
        Assert.NotNull(u.Id);
        //Assert.NotNull(u.Username);
        //Assert.NotNull(u.Password);
        //Assert.NotNull(u.FullName);
        //Assert.NotNull(u.Email);
        Assert.NotNull(u.AccessLevel);
    }

    [Fact]
    public void ShouldSetUsername()
    {
        User u = new();
        u.Username = "Me";
        Assert.Equal("Me", u.Username);
    }
    [Fact]
    public void ShouldSetPassword()
    {
        User u = new();
        u.Password = "password";
        Assert.Equal("password", u.Password);
    }
    [Fact]
    public void ShouldSetFullName()
    {
        User u = new();
        u.FullName = "Bob Jones";
        Assert.Equal("Bob Jones", u.FullName);
    }
    [Fact]
    public void ShouldSetEmail()
    {
        User u = new();
        u.Email = "This@mail.com";
        Assert.Equal("This@mail.com", u.Email);
    }
    [Fact]
    public void UserConstructorOneShouldCreate()
    {
        User u = new User(1, "name", "hello", "name name", "email", 1);

        Assert.NotNull(u);
        Assert.NotNull(u.Id);
        Assert.NotNull(u.Username);
        Assert.NotNull(u.Password);
        Assert.NotNull(u.FullName);
        Assert.NotNull(u.Email);
        Assert.NotNull(u.AccessLevel);
    }
    [Fact]
    public void UserConstructorTwoShouldCreate()
    {
        User u = new User("name", "hello", "name name", "email", 1);

        Assert.NotNull(u);
        Assert.NotNull(u.Username);
        Assert.NotNull(u.Password);
        Assert.NotNull(u.FullName);
        Assert.NotNull(u.Email);
        Assert.NotNull(u.AccessLevel);
    }
    [Fact]
    public void UserConstructorThreeShouldCreate()
    {
        User u = new User("name", "hello", "name name", "email");

        Assert.NotNull(u);
        Assert.NotNull(u.Username);
        Assert.NotNull(u.Password);
        Assert.NotNull(u.FullName);
        Assert.NotNull(u.Email);
    }
    [Fact]
    public void UserConstructorFourShouldCreate()
    {
        User u = new User();

        Assert.NotNull(u);
    }
}