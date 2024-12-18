namespace CareTrack.Server.Modules.Infrastructure.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public Role Role { get; set; } = Role.Patient;
    public string PasswordHash { get; set; } = string.Empty;
}

public enum Role
{
    Doctor,
    Patient
}