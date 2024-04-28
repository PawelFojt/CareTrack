namespace CareTrack.Domain.Models;
public class Recipe
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int MedicineId { get; set; }
    public int Quantity { get; set; }
    public IEnumerable<TimeOnly> DosingTime { get; set; } = [];
}
