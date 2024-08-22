using CareTrack.Server.Modules.Domain.Models;

namespace CareTrack.Server.Modules.Infrastructure.Entities;
public class Medicine : IMedicine
{
    public int Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public DateOnly ExpirationDate { get; set; } 
}
