
namespace CareTrack.Server.Modules.Infrastructure.Entities;
public class PrescriptionMedicine
{
    public int MedicineId { get; set; }
    public int PrescriptionId { get; set; }

    public Medicine Medicine { get; set; } = new();
    public Prescription Prescription { get; set; } = new();
}
