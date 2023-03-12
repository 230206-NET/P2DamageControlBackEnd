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
    public User RetrieveUsername(string username)
    {
        List<User> Users = _repo.GetAllUsers();
        User filteredUser;
        try
        {
            filteredUser = Users.First(user => user.Username.ToLower().Equals(username.ToLower()));
        }
        catch (InvalidOperationException)
        {
            filteredUser = null;
        }
        return filteredUser;
    }

    //If the entered username is in the system, checks that the password entered equals the password for that username, and returns true if so
    public Boolean Login(string username, string password)
    {
        User user = RetrieveUsername(username);

        if (user == null)
        {
            return false;
        }
        else if (user.Password.Equals(password))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Return false if the username entered is not in the database
    public boolean UsernameExists(string username)
    {
        if (RetrieveUsername(username) != null)
        {
            return true;
        }
        return false;
    }

    //Stores information of a new user
    public User CreateAccount(User newUser)
    {
        _repo.CreateAccount(newUser);
        return newUser;
    }
}