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
    /// Persists a new ticket to storage
    /// </summary>
    /// <returns>nothing -- for now</returns>
    void CreateNewUser(User newUser);
}