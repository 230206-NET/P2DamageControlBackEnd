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
    // public List<User> GetAllUsers()
    // {
    //     return _repo.GetAllUsers();
    // }

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

    public void UpdateTicketStatus(int TicketID, int TicketStatus)
    {
        _repo.UpdateTicketStatus(TicketID, TicketStatus);
    }

    public Ticket UpdateTicketStatusWithUserId(int userID, int TicketId, int TicketStatus)
    {
        User editor = GetUserByUserId(userID);

        if (editor == null || editor.AccessLevel == 0)
        {
            return null;
        }

        Ticket ticketToEdit = GetTicketById(TicketId);

        if (ticketToEdit != null && ticketToEdit.TicketStatus == 0 && ticketToEdit.Amount <= 100 && editor.AccessLevel >= 1)
        {
            UpdateTicketStatus(ticketToEdit.Id, TicketStatus);
            return GetTicketById(ticketToEdit.Id);
        }
        else if (ticketToEdit != null && ticketToEdit.TicketStatus == 0 && ticketToEdit.Amount <= 1000 && editor.AccessLevel >= 2)
        {
            UpdateTicketStatus(ticketToEdit.Id, TicketStatus);
            return GetTicketById(ticketToEdit.Id);
        }
        else if (ticketToEdit != null && ticketToEdit.TicketStatus == 0 && ticketToEdit.Amount <= 10000 && editor.AccessLevel >= 3)
        {
            UpdateTicketStatus(ticketToEdit.Id, TicketStatus);
            return GetTicketById(ticketToEdit.Id);
        }
        else if (ticketToEdit != null && ticketToEdit.TicketStatus == 0 && editor.AccessLevel >= 4)
        {
            UpdateTicketStatus(ticketToEdit.Id, TicketStatus);
            return GetTicketById(ticketToEdit.Id);
        }
        else
        {
            return null;
        }
    }
}