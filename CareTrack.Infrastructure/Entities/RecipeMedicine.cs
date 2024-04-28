
namespace CareTrack.Infrastructure.Entities;
public class RecipeMedicine
{
    public int MedicineId { get; set; }
    public List<Medicine> Medicines { get; set; } = [];

    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; } = new();
}
