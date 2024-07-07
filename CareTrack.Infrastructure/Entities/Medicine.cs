using CareTrack.Domain.Models;

namespace CareTrack.Infrastructure.Entities;
public class Medicine : IMedicine
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Quantity { get; set; }
    public DateOnly? ExpirationDate { get; set; } 
}
