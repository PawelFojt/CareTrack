using CareTrack.Server.Modules.Infrastructure.Entities;

namespace CareTrack.Server.Modules.Domain.Models;

public interface IEvent
{
    int Id { get; set; }
    string? Name { get; set; }
    string? Description { get; set; }
    DateTime? Date { get; set; }
    int PatientId { get; set; }
}

public class EventResult : IEvent
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime? Date { get; set; }
    public int PatientId { get; set; }
}