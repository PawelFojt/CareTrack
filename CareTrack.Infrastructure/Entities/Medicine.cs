namespace CareTrack.Infrastructure.Entities;
public class Medicine
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Quantity { get; set; }
    public DateOnly? ExpirationDate { get; set; } 
}
