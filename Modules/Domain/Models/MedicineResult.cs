namespace CareTrack.Server.Modules.Domain.Models;
public interface IMedicine
{
    int Id { get; set; }
    string Name { get; set; }
    int Quantity { get; set; }
    DateOnly ExpirationDate { get; set; }
}

public class MedicineResult : IMedicine
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public DateOnly ExpirationDate { get; set; }
}
