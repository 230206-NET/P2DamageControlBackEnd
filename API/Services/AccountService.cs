using Models;
using Data;

namespace Services;

public class AccountService
{
    //Dependency injection
    private readonly IRepository _repo;
    public AccountService(IRepository repo)
    {
        _repo = repo;
    }

    //Get a list of all users in the system
    public List<User> GetAllUsers()
    {
        return _repo.GetAllUsers();
    }

    //Check for a username in the database that equals some entered username
    public User? GetUserByUsername(string username)
    {
        return _repo.GetUserByUsername(username);
    }

    public User? GetUserByUserId(int id)
    {
        return _repo.GetUserByUserId(id);
    }



    //Return false if the username entered is not in the database
    public Boolean UsernameExists(string username)
    {
        if (GetUserByUsername(username) != null)
        {
            return true;
        }
        return false;
    }

    //Stores information of a new user
    public User CreateNewUser(User newUser)
    {
        newUser.Password = PasswordService.HashAndSaltPassword(newUser.Password);
        _repo.CreateNewUser(newUser);
        return newUser;
    }

    public List<Ticket> GetAllTickets()
    {
        return _repo.GetAllTickets();
    }

    //Returns a ticket with the given ID
    public Ticket GetTicketById(int ID)
    {
        List<Ticket> tickets = _repo.GetAllTickets();
        Ticket? filteredTicket;
        try
        {
            filteredTicket = tickets.First(ticket => ticket.Id.Equals(ID));
        }
        catch (InvalidOperationException)
        {
            filteredTicket = null;
        }
        return filteredTicket;
    }

    public List<Ticket> GetTicketsByUserId(int Id)
    {
        return _repo.GetTicketsByUserId(Id);
    }

    public List<Ticket> GetPendingTickets()
    {
        return _repo.GetPendingTickets();
    }

    public Ticket CreateNewTicket(Ticket newTicket)
    {
        _repo.CreateNewTicket(newTicket);
        return newTicket;
    }

    //Used to change ticket status to either accepted or rejected, allows for the input of some justification for the ruling, and applies the employee ID to the ticket to show who made the decision
    public Ticket UpdateTicketStatus(int TicketID, int TicketStatus, string TicketJustification, int userID)
    {
        _repo.UpdateTicketStatus(TicketID, TicketStatus, TicketJustification, userID);
        return GetTicketById(TicketID);
    }

    //Finds a user with the given userID, finds a ticket with the given ticketId, and checks if the user has the authorization
    //required to make a ruling on the ticket. If so, runs UpdateTicketStatus, if not, returns null.
    public Ticket UpdateTicketStatusWithUserId(int userID, int TicketId, int TicketStatus, string TicketJustification)
    {
        User editor = GetUserByUserId(userID);

        if (editor == null || editor.AccessLevel == 0)
        {
            return null;
        }

        Ticket ticketToEdit = GetTicketById(TicketId);

        if (ticketToEdit != null && ticketToEdit.TicketStatus == 0 && ticketToEdit.Amount <= 100 && editor.AccessLevel >= 1)
        {
            UpdateTicketStatus(ticketToEdit.Id, TicketStatus, TicketJustification, userID);
            return GetTicketById(ticketToEdit.Id);
        }
        else if (ticketToEdit != null && ticketToEdit.TicketStatus == 0 && ticketToEdit.Amount <= 1000 && editor.AccessLevel >= 2)
        {
            UpdateTicketStatus(ticketToEdit.Id, TicketStatus, TicketJustification, userID);
            return GetTicketById(ticketToEdit.Id);
        }
        else if (ticketToEdit != null && ticketToEdit.TicketStatus == 0 && ticketToEdit.Amount <= 10000 && editor.AccessLevel >= 3)
        {
            UpdateTicketStatus(ticketToEdit.Id, TicketStatus, TicketJustification, userID);
            return GetTicketById(ticketToEdit.Id);
        }
        else
        {
            return null;
        }
    }

    //Used to change a user's access level, for promoting and demoting employees
    public void UpdateUserAccessLevel(int UserID, int AccessLevel)
    {
        _repo.UpdateUserAccessLevel(UserID, AccessLevel);
    }

    //Checks if the user with the given AdminID is an admin, if so will find user with the given UserId and allow admin to
    //adjust that user's acces level. If not, returns null
    public User UpdateUserAccessLevelWithUserId(int AdminID, int UserId, int AccessLevel)
    {
        User admin = GetUserByUserId(AdminID);

        if (admin == null || admin.AccessLevel != 3)
        {
            return null;
        }

        User userToEdit = GetUserByUserId(UserId);

        if (userToEdit != null)
        {
            UpdateUserAccessLevel(userToEdit.Id, AccessLevel);
            return GetUserByUserId(userToEdit.Id);
        }
        else
        {
            return null;
        }
    }
}