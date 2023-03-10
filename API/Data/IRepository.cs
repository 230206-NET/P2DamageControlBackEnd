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
}