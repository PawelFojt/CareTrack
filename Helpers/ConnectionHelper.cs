using Npgsql;

namespace CareTrack.Server.Helpers;

public static class ConnectionHelper
{
    public static string GetConnectionString(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("CareTrackDb");
        var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
       return (string.IsNullOrEmpty(databaseUrl) ? connectionString : BuildConnectionString(databaseUrl)) ?? 
              throw new InvalidOperationException("Cannot get connection string from database.");
    }
    
    private static string BuildConnectionString(string databaseUrl)
    {
        var databaseUri = new Uri(databaseUrl);
        var userInfo = databaseUri.UserInfo.Split(':');
        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = databaseUri.Host,
            Port = databaseUri.Port,
            Username = userInfo[0],
            Password = userInfo[1],
            Database = databaseUri.LocalPath.TrimStart('/'),
            SslMode = SslMode.Require
        };
        return builder.ToString();
    }
}