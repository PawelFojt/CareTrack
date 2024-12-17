using CareTrack.Server.Modules.Domain.Models;

namespace CareTrack.Server.Modules.Infrastructure.Entities;
public class Prescription : IPrescription
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public List<TimeOnly> DosingTime { get; set; } = new();
    public virtual ICollection<PrescriptionMedicine> PrescriptionMedicines { get; set; } = [];
}
