using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;

namespace Services;

public static class PasswordServices
{
    public static string HashAndSaltPassword(string password)
    {
        byte[] salt = CreateSalt();

        byte[] hash = ComputeHash(password, salt);

        string saltStr = Convert.ToBase64String(salt);
        string hashStr = Convert.ToBase64String(hash);

        return $"{saltStr}:{hashStr}";
    }

    private static byte[] CreateSalt()
    {
        byte[] salt = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }

    private static byte[] ComputeHash(string password, byte[] salt)
    {
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
        {
            return pbkdf2.GetBytes(32);
        }

    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        string[] split = hashedPassword.Split(':');
        string saltStr = split[0];
        string hashStr = split[1];

        byte[] salt = Convert.FromBase64String(saltStr);
        byte[] hash = Convert.FromBase64String(hashStr);

        byte[] enteredHash = ComputeHash(password, salt);

        return SlowEquals(hash, enteredHash);
    }

    private static bool SlowEquals(byte[] a, byte[] b)
    {
        uint diff = (uint)a.Length ^ (uint)b.Length;
        for (int i = 0; i < a.Length && i < b.Length; i++)
        {
            diff |= (uint)(a[i] ^ b[i]);
        }
        return diff == 0;
    }
}