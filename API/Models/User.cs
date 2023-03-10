namespace Models;
public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public int AccessLevel { get; set; } = 0;

    public User(int Id, string Username, string Password, string FullName, string Email, int AccessLevel)
    {
        this.Id = Id;
        this.Username = Username;
        this.Password = Password;
        this.FullName = FullName;
        this.Email = Email;
        this.AccessLevel = AccessLevel;
    }

    public User(string Username, string Password, string FullName, string Email, int AccessLevel)
    {
        this.Username = Username;
        this.Password = Password;
        this.FullName = FullName;
        this.Email = Email;
        this.AccessLevel = AccessLevel;
    }

    public User(string Username, string Password, string FullName, string Email)
    {
        this.Username = Username;
        this.Password = Password;
        this.FullName = FullName;
        this.Email = Email;
    }
}