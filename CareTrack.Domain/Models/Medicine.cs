namespace CareTrack.Domain.Models;
public class Medicine
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Quantity { get; set; } = string.Empty;
    public string ExpirationDate { get; set; } = string.Empty;
}
