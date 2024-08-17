namespace CareTrack.Domain.Models;
public interface IMedicine
{
    int Id { get; set; }
    string Name { get; set; }
    int Quantity { get; set; }
    DateOnly? ExpirationDate { get; set; }
}

public class Medicine : IMedicine
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public DateOnly? ExpirationDate { get; set; }
}
