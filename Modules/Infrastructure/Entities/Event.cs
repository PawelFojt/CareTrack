using CareTrack.Server.Modules.Domain.Models;

namespace CareTrack.Server.Modules.Infrastructure.Entities;


public class Event : IEvent
{
    public int Id { get; set; }
    public string? Name { get; set; } 
    public string? Description { get; set; }
    public DateTime? Date { get; set; }
    public int PatientId { get; set; }
    public virtual Patient? Patient { get; set; }
}