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
        List<Ticket> allTickets = new List<Ticket>();

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
                (string)reader["Ticket_Damager_Info"]
            )
            );
        }
        return allTickets;
    }

    public User? GetUserByUsername(string Username)
    {


        using SqlConnection conn = new(Secrets.getConnectionString());
        conn.Open();

        using SqlCommand cmd = new("SELECT TOP 1 * FROM Users WHERE UPPER(Username) = @Username", conn);
        cmd.Parameters.AddWithValue("@Username", Username.ToUpper());
        using SqlDataReader reader = cmd.ExecuteReader();


        while (reader.Read())
        {
            User? user = new User(
                (int)reader["User_ID"],
                (string)reader["Username"],
                (string)reader["Hashed_Password"],
                (string)reader["Full_Name"],
                (string)reader["Email"],
                (int)reader["Access_Level"]

            );
            return user;
        }
        return null;
    }

    /// <summary>
    /// Persists a new ticket to storage
    /// </summary>
    // public void CreateNewTicket(Ticket newTicket)
    // {
    //     using SqlConnection conn = new(Secrets.getConnectionString());
    //     conn.Open();

    //     using SqlCommand cmd = new("INSERT into Tickets(Ticket_Num, Ticket_Amount, Ticket_Client_ID, Ticket_Submission_Date, Ticket_Damage_Date, Ticket_Description, Ticket_Damager_Info) Values (@Id, @Amount, @ClientId, @SubmissionDate, @DamageDate, @Description, @DamagerId)", conn);
    //     cmd.Parameters.AddWithValue("@Id", newTicket.Id);
    //     cmd.Parameters.AddWithValue("@Amount", newTicket.Amount);
    //     cmd.Parameters.AddWithValue("@ClientId", newTicket.ClientId);
    //     cmd.Parameters.AddWithValue("@SubmissionDate", newTicket.SubmissionDate);
    //     cmd.Parameters.AddWithValue("@DamageDate", newTicket.DamageDate);
    //     cmd.Parameters.AddWithValue("@Description", newTicket.Description);
    //     cmd.Parameters.AddWithValue("@DamagerId", newTicket.DamagerId);

    //     cmd.ExecuteNonQuery();
    // }
    public void CreateNewTicket(Ticket newTicket)
    {
        using SqlConnection conn = new(Secrets.getConnectionString());
        conn.Open();

        using SqlCommand cmd = new("INSERT into Tickets(Ticket_Amount, Ticket_Client_ID, Ticket_Submission_Date, Ticket_Damage_Date, Ticket_Description, Ticket_Damager_Info, Ticket_Status) Values (@Amount, @ClientId, @SubmissionDate, @DamageDate, @Description, @DamagerId, 0)", conn);
        //cmd.Parameters.AddWithValue("@Id", newTicket.Id);
        cmd.Parameters.AddWithValue("@Amount", newTicket.Amount);
        cmd.Parameters.AddWithValue("@ClientId", newTicket.ClientId);
        cmd.Parameters.AddWithValue("@SubmissionDate", newTicket.SubmissionDate);
        cmd.Parameters.AddWithValue("@DamageDate", newTicket.DamageDate);
        cmd.Parameters.AddWithValue("@Description", newTicket.Description);
        cmd.Parameters.AddWithValue("@DamagerId", newTicket.DamagerId);

        cmd.ExecuteNonQuery();
    }

    public void CreateNewUser(User newUser)
    {
        using SqlConnection conn = new(Secrets.getConnectionString());
        conn.Open();

        using SqlCommand cmd = new("INSERT into Users(Username, Hashed_Password, Full_Name, Email, AccessLevel) Values (@Username, @Hashed_Password, @Full_Name, @Email, @AccessLevel)", conn);
        cmd.Parameters.AddWithValue("@Username", newUser.Username);
        cmd.Parameters.AddWithValue("@Hashed_Password", newUser.Password);
        cmd.Parameters.AddWithValue("@Full_Name", newUser.FullName);
        cmd.Parameters.AddWithValue("@Email", newUser.Email);
        cmd.Parameters.AddWithValue("@AccessLevel", newUser.AccessLevel);

        cmd.ExecuteNonQuery();
    }
}