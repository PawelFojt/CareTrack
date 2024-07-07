namespace CareTrack.Domain.Models;
public class Medicine : IMedicine
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Quantity { get; set; } = string.Empty;
    public DateOnly? ExpirationDate { get; set; }
}

public interface IMedicine
{
    int Id { get; set; }
    string Name { get; set; }
    string Quantity { get; set; }
    DateOnly? ExpirationDate { get; set; }
}
