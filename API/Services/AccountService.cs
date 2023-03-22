using Models;
using Data;
using System.Data.SqlClient;

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
        try
        {
            return _repo.GetAllUsers();
        }
        catch (SqlException exception)
        {
            throw exception;
        }
    }
    //updates the user's information
    public User UpdateUserInfo(User user)
    {
        try
        {
            _repo.UpdateUserInfo(user);
            return user;
        }
        catch (SqlException exception)
        {
            throw exception;
        }
    }

    //Check for a username in the database that equals some entered username
    public User? GetUserByUsername(string username)
    {
        try
        {
            return _repo.GetUserByUsername(username);
        }
        catch (SqlException exception)
        {
            throw exception;
        }
    }

    public User? GetUserByUserId(int id)
    {
        try
        {
            return _repo.GetUserByUserId(id);
        }
        catch (SqlException exception)
        {
            throw exception;
        }
    }



    //Return false if the username entered is not in the database
    public Boolean UsernameExists(string username)
    {
        try
        {
            if (GetUserByUsername(username) != null)
            {
                return true;
            }
            return false;
        }
        catch (SqlException exception)
        {
            throw exception;
        }
    }

    //Stores information of a new user
    public User CreateNewUser(User newUser)
    {
        try
        {
            newUser.Password = PasswordService.HashAndSaltPassword(newUser.Password);
            _repo.CreateNewUser(newUser);
            return newUser;
        }
        catch (SqlException exception)
        {
            throw exception;
        }
    }

    //Get all tickets from  database repo
    public List<Ticket> GetAllTickets()
    {
        try
        {
            return _repo.GetAllTickets();
        }
        catch (SqlException exception)
        {
            throw exception;
        }
    }

    //Returns a ticket with the given ID
    public Ticket? GetTicketById(int ID)
    {
        List<Ticket> tickets = new();
        try
        {
            tickets = _repo.GetAllTickets();
        }
        catch (SqlException exception)
        {
            throw exception;
        }
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

    //gets a list of tickets using an user Id
    public List<Ticket> GetTicketsByUserId(int Id)
    {
        try
        {
            return _repo.GetTicketsByUserId(Id);
        }
        catch (SqlException exception)
        {
            throw exception;
        }
    }

    //gets a list of pending tickets
    public List<Ticket> GetPendingTickets()
    {
        try
        {
            return _repo.GetPendingTickets();
        }
        catch (SqlException exception)
        {
            throw exception;
        }
    }

    //persists a new ticket to storage
    public Ticket CreateNewTicket(Ticket newTicket)
    {
        try
        {
            _repo.CreateNewTicket(newTicket);
            return newTicket;
        }
        catch (SqlException exception)
        {
            throw exception;
        }
    }

    //Used to change ticket status to either accepted or rejected, allows for the input of some justification for the ruling, and applies the employee ID to the ticket to show who made the decision
    public Ticket? UpdateTicketStatus(int TicketID, int TicketStatus, string TicketJustification, int userID)
    {
        try
        {
            _repo.UpdateTicketStatus(TicketID, TicketStatus, TicketJustification, userID);
            return GetTicketById(TicketID);
        }
        catch (SqlException exception)
        {
            throw exception;
        }
    }

    //Finds a user with the given userID, finds a ticket with the given ticketId, and checks if the user has the authorization
    //required to make a ruling on the ticket. If so, runs UpdateTicketStatus, if not, returns null.
    public Ticket? UpdateTicketStatusWithUserId(int userID, int TicketId, int TicketStatus, string TicketJustification)
    {
        User? editor;
        Ticket? ticketToEdit;
        try
        {
            editor = GetUserByUserId(userID);
            ticketToEdit = GetTicketById(TicketId);
        }
        catch (SqlException exception)
        {
            throw exception;
        }

        if (editor == null || editor.AccessLevel == 0)
        {
            return null;
        }



        if (ticketToEdit != null && ticketToEdit.TicketStatus == 0)
        {
            if (ticketToEdit.Amount <= 100 && editor.AccessLevel >= 1
                || ticketToEdit.Amount <= 1000 && editor.AccessLevel >= 2
                || editor.AccessLevel >= 3)
            {
                try
                {
                    UpdateTicketStatus(ticketToEdit.Id, TicketStatus, TicketJustification, userID);
                    return GetTicketById(ticketToEdit.Id);
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
        }
        return null;
    }

    //Used to change a user's access level, for promoting and demoting employees
    public void UpdateUserAccessLevel(int UserID, int AccessLevel)
    {
        try
        {
            _repo.UpdateUserAccessLevel(UserID, AccessLevel);
        }
        catch (SqlException exception)
        {
            throw exception;
        }
    }

    public void DeclineAllPendingTicketsForUserId(int userId)
    {
        try
        {
            _repo.DeclineAllPendingTicketsForUserId(userId);
        }
        catch (SqlException exception)
        {
            throw exception;
        }
    }

    //Checks if the user with the given AdminID is an admin, if so will find user with the given UserId and allow admin to
    //adjust that user's acces level. If not, returns null
    public User? UpdateUserAccessLevelWithUserId(int AdminID, int UserId, int AccessLevel)
    {
        User? admin;
        User? userToEdit;
        try
        {
            admin = GetUserByUserId(AdminID);
            userToEdit = GetUserByUserId(UserId);
        }
        catch (SqlException exception)
        {
            throw exception;
        }

        if (admin == null || admin.AccessLevel != 3)
        {
            return null;
        }

        if (userToEdit != null)
        {
            try
            {
                UpdateUserAccessLevel(userToEdit.Id, AccessLevel);
                DeclineAllPendingTicketsForUserId(userToEdit.Id);
                return GetUserByUserId(userToEdit.Id);
            }
            catch (SqlException exception)
            {
                throw exception;
            }
        }
        else
        {
            return null;
        }
    }
}