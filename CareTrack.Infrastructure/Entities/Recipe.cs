namespace CareTrack.Infrastructure.Entities;
public class Recipe
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public IEnumerable<TimeOnly> DosingTime { get; set; } = [];
    public virtual ICollection<RecipeMedicine>? RecipeMedicines { get; set; }
}
