using Models;
using System.Collections.Generic;
namespace Data;
public interface IRepository
{
    /// <summary>
    /// Retrieves all tickets
    /// </summary>
    /// <returns>a List of tickets</returns>
    List<Ticket> GetAllTickets();
    /// <summary>
    /// Retrieves all Users
    /// </summary>
    /// <returns>a List of users</returns>
    List<User> GetAllUsers();

    /// <summary>
    /// Persists a new ticket to storage
    /// </summary>
    /// <returns>nothing -- for now</returns>
    void CreateNewTicket(Ticket newTicket);

    /// <summary>
    /// Retrieves an User by its username.
    /// </summary>
    /// <returns>an User object</returns>
    User? GetUserByUsername(string Username);
    /// <summary>
    /// Retrieves an User by its userId.
    /// </summary>
    /// <returns>an User object</returns>
    User? GetUserByUserId(int Id);

    /// <summary>
    /// Persists a new user to storage
    /// </summary>
    /// <returns>nothing -- for now</returns>
    void CreateNewUser(User newUser);

    /// <summary>
    /// Retrieves a ticket to approve or deny, along with entering some justification and applying the employee ID to the ticket
    /// </summary>
    /// <returns>nothing -- for now</returns>
    void UpdateTicketStatus(int TicketID, int TicketStatus, string TicketJustification, int userID);
    /// <summary>
    /// Retrieves a user to promote or demote
    /// </summary>
    /// <returns>nothing -- for now</returns>
    void UpdateUserAccessLevel(int UserID, int AccessLevel);
    /// <summary>
    /// Retrieves a list of tickets for the specified user
    /// </summary>
    /// <returns>a Ticket List</returns>
    List<Ticket> GetTicketsByUserId(int Id);
    /// <summary>
    /// Retrieves a list of pending tickets
    /// </summary>
    /// <returns>a Ticket List</returns>
    List<Ticket> GetPendingTickets();

}