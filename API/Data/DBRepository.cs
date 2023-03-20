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
                reader["Ticket_Employee_ID"] == DBNull.Value ? -1 : (int)reader["Ticket_Employee_ID"],
                (DateTime)reader["Ticket_Submission_Date"],
                (DateTime)reader["Ticket_Damage_Date"],
                (string)reader["Ticket_Description"],
                (string)reader["Ticket_Damager_Info"],
                reader["Ticket_Justification"] == DBNull.Value ? " " : (string)reader["Ticket_Justification"],
                (int)reader["Ticket_Status"]
            )
            );
        }
        return allTickets;
    }

    /// <summary>
    /// Retrieves an Tickets by an userId.
    /// </summary>
    /// <returns>an List of Tickets</returns>
    public List<Ticket> GetTicketsByUserId(int Id)
    {
        List<Ticket> filteredTickets = new List<Ticket>();

        using SqlConnection conn = new(Secrets.getConnectionString());
        conn.Open();

        using SqlCommand cmd = new("SELECT * FROM Tickets WHERE Ticket_Client_ID = @UserId", conn);
        cmd.Parameters.AddWithValue("@UserId", Id);
        using SqlDataReader reader = cmd.ExecuteReader();


        while (reader.Read())
        {
            filteredTickets.Add(new Ticket(
                (int)reader["Ticket_Num"],
                (decimal)reader["Ticket_Amount"],
                (int)reader["Ticket_Client_ID"],
                reader["Ticket_Employee_ID"] == DBNull.Value ? -1 : (int)reader["Ticket_Employee_ID"],
                (DateTime)reader["Ticket_Submission_Date"],
                (DateTime)reader["Ticket_Damage_Date"],
                (string)reader["Ticket_Description"],
                (string)reader["Ticket_Damager_Info"],
                reader["Ticket_Justification"] == DBNull.Value ? " " : (string)reader["Ticket_Justification"],
                (int)reader["Ticket_Status"]
            )
            );
        }
        return filteredTickets;
    }

    public List<Ticket> GetPendingTickets()
    {
        List<Ticket> filteredTickets = new List<Ticket>();

        using SqlConnection conn = new(Secrets.getConnectionString());
        conn.Open();

        using SqlCommand cmd = new("SELECT * FROM Tickets WHERE Ticket_Status = 0", conn);
        //cmd.Parameters.AddWithValue("@TicketStatus", TicketStatus); maybe for later use.
        using SqlDataReader reader = cmd.ExecuteReader();


        while (reader.Read())
        {
            filteredTickets.Add(new Ticket(
                (int)reader["Ticket_Num"],
                (decimal)reader["Ticket_Amount"],
                (int)reader["Ticket_Client_ID"],
                reader["Ticket_Employee_ID"] == DBNull.Value ? -1 : (int)reader["Ticket_Employee_ID"],
                (DateTime)reader["Ticket_Submission_Date"],
                (DateTime)reader["Ticket_Damage_Date"],
                (string)reader["Ticket_Description"],
                (string)reader["Ticket_Damager_Info"],
                reader["Ticket_Justification"] == DBNull.Value ? " " : (string)reader["Ticket_Justification"],
                (int)reader["Ticket_Status"]
            )
            );
        }
        return filteredTickets;
    }

    /// <summary>
    /// Retrieves all Users
    /// </summary>
    /// <returns>a List of users</returns>
    public List<User> GetAllUsers()
    {
        List<User> allUsers = new List<User>();

        using SqlConnection conn = new(Secrets.getConnectionString());
        conn.Open();

        using SqlCommand cmd = new("SELECT * FROM Users", conn);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            allUsers.Add(new User(
                (int)reader["User_ID"],
                (string)reader["Username"],
                (string)reader["Hashed_Password"],
                (string)reader["Full_Name"],
                (string)reader["Email"],
                (int)reader["Access_Level"]
            )
            );
        }
        return allUsers;
    }
    /// <summary>
    /// Retrieves an User by its username.
    /// </summary>
    /// <returns>an User object</returns>
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
    /// Retrieves an User by its userId.
    /// </summary>
    /// <returns>an User object</returns>
    public User? GetUserByUserId(int Id)
    {


        using SqlConnection conn = new(Secrets.getConnectionString());
        conn.Open();

        using SqlCommand cmd = new("SELECT TOP 1 * FROM Users WHERE User_ID = @UserId", conn);
        cmd.Parameters.AddWithValue("@UserId", Id);
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
    /// <summary>
    /// Persists a new ticket to storage
    /// </summary>
    /// <returns>nothing -- for now</returns>
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

    public void DeclineAllPendingTicketsForUserId(int UserId)
    {
        using SqlConnection conn = new(Secrets.getConnectionString());
        conn.Open();

        using SqlCommand cmd = new("UPDATE Tickets SET Ticket_Status = 2 WHERE Ticket_Client_ID = @UserId AND Ticket_Status = 0", conn);
        cmd.Parameters.AddWithValue("@UserId", UserId);

        cmd.ExecuteNonQuery();
    }

    /// <summary>
    /// Persists a new user to storage
    /// </summary>
    /// <returns>nothing -- for now</returns>
    public void CreateNewUser(User newUser)
    {
        using SqlConnection conn = new(Secrets.getConnectionString());
        conn.Open();

        using SqlCommand cmd = new("INSERT into Users(Username, Hashed_Password, Full_Name, Email, Access_Level) Values (@Username, @Hashed_Password, @Full_Name, @Email, @AccessLevel)", conn);
        cmd.Parameters.AddWithValue("@Username", newUser.Username);
        cmd.Parameters.AddWithValue("@Hashed_Password", newUser.Password);
        cmd.Parameters.AddWithValue("@Full_Name", newUser.FullName);
        cmd.Parameters.AddWithValue("@Email", newUser.Email);
        cmd.Parameters.AddWithValue("@AccessLevel", newUser.AccessLevel);

        cmd.ExecuteNonQuery();
    }
    /// <summary>
    /// Retrieves a ticket to approve or deny, along with entering some justification and applying the employee ID to the ticket
    /// </summary>
    /// <returns>nothing -- for now</returns>
    public void UpdateTicketStatus(int TicketID, int TicketStatus, string TicketJustification, int UserID)
    {
        using SqlConnection conn = new(Secrets.getConnectionString());
        conn.Open();

        using SqlCommand cmd = new("UPDATE Tickets SET Ticket_Employee_ID = @UserId, Ticket_Status = @tStatus, Ticket_Justification = @tJustification Where Ticket_Num = @tId", conn);
        cmd.Parameters.AddWithValue("@tId", TicketID);
        cmd.Parameters.AddWithValue("@UserId", UserID);
        cmd.Parameters.AddWithValue("@tStatus", TicketStatus);
        cmd.Parameters.AddWithValue("@tJustification", TicketJustification);

        cmd.ExecuteNonQuery();
    }
    /// <summary>
    /// Retrieves a user to promote or demote
    /// </summary>
    /// <returns>nothing -- for now</returns>
    public void UpdateUserAccessLevel(int UserID, int AccessLevel)
    {
        using SqlConnection conn = new(Secrets.getConnectionString());
        conn.Open();

        using SqlCommand cmd = new("UPDATE Users SET Access_Level = @uLevel Where User_ID = @uId", conn);
        cmd.Parameters.AddWithValue("@uId", UserID);
        cmd.Parameters.AddWithValue("@uLevel", AccessLevel);

        cmd.ExecuteNonQuery();
    }
    public void UpdateUserInfo(User user)
    {
        using SqlConnection conn = new(Secrets.getConnectionString());
        conn.Open();
        Console.WriteLine("About to change info");
        if (user.Password != "N/A")
        {
            Console.WriteLine("Password updated");
            using SqlCommand cmd = new SqlCommand("Update Users SET Username = @Username, Hashed_Password = @Hashed_Password, Full_Name = @Full_Name, Email = @Email WHERE User_ID = @uID", conn);
            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters.AddWithValue("@Hashed_Password", user.Password);
            cmd.Parameters.AddWithValue("@Full_Name", user.FullName);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@uId", user.Id);
            cmd.ExecuteNonQuery();
        }
        else
        {
            using SqlCommand cmd = new SqlCommand("Update Users SET Username = @Username, Full_Name = @Full_Name, Email = @Email WHERE User_ID = @uID", conn);
            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters.AddWithValue("@Full_Name", user.FullName);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@uId", user.Id);
            Console.WriteLine("password not updated");
            cmd.ExecuteNonQuery();
        }
    }
}