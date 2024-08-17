using CareTrack.Domain.Models;

namespace CareTrack.Infrastructure.Entities;
public class Prescription : IPrescription
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public List<TimeOnly> DosingTime { get; set; } = [];
    public virtual ICollection<PrescriptionMedicine>? PrescriptionMedicines { get; set; }
}
