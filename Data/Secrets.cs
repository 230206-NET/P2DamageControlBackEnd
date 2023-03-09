namespace Data;

internal class Secrets
{

    private const string _connectionString = "Server=tcp:230206net-p2-server.database.windows.net,1433;Initial Catalog=TeamA;Persist Security Info=False;User ID=teamA;Password=d@mag5C0ntrol!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

    public static string getConnectionString() => _connectionString;
}