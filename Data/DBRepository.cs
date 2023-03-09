using Models;
using System.Data.SqlClient;

namespace Data;

public class DBRepository : IRepository
{
    /// <summary>
    /// Retrieves all tickets
    /// </summary>
    /// <returns>a List of tickets</returns>
    public List<Ticket> GetAllTickets()
    {
        List<Ticket> allTickets = new();

        using SqlConnection conn = new(Secrets.getConnectionString());
        conn.Open();

        using SqlCommand cmd = new("SELECT * FROM Tickets", conn);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            allTickets.Add(new Ticket(
                (int)reader["Ticket_Num"],
                (decimal)reader["Ticket_Amount"],
                (int)reader["Ticket_Client_ID"],
                (DateTime)reader["Ticket_Submission_Date"],
                (DateTime)reader["Ticket_Damage_Date"],
                (string)reader["Ticket_Description"],
                (int)reader["Ticket_Damager_Info"]
            )
            );
        }
        return allTickets;
    }

    /// <summary>
    /// Persists a new ticket to storage
    /// </summary>
    public void CreateNewTicket(Ticket newTicket)
    {
        using SqlConnection conn = new(Secrets.getConnectionString());
        conn.Open();

        using SqlCommand cmd = new("INSERT into Tickets(Ticket_Num, Ticket_Amount, Ticket_Client_ID, Ticket_Submission_Date, Ticket_Damage_Date, Ticket_Description, Ticket_Damager_Info) Values (@Id, @Amount, @ClientId, @SubmissionDate, @DamageDate, @Description, @DamagerId)", conn);
        cmd.Parameters.AddWithValue("@Id", newTicket.Id);
        cmd.Parameters.AddWithValue("@Amount", newTicket.Amount);
        cmd.Parameters.AddWithValue("@ClientId", newTicket.ClientId);
        cmd.Parameters.AddWithValue("@SubmissionDate", newTicket.SubmissionDate);
        cmd.Parameters.AddWithValue("@DamageDate", newTicket.DamageDate);
        cmd.Parameters.AddWithValue("@Description", newTicket.Description);
        cmd.Parameters.AddWithValue("@DamagerId", newTicket.DamagerId);

        cmd.ExecuteNonQuery();
    }
}