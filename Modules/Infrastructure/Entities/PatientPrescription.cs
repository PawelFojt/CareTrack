
namespace CareTrack.Server.Modules.Infrastructure.Entities;
public class PatientPrescription 
{
    public int PrescriptionId { get; set; }
    public int PatientId { get; set; }
    public virtual Prescription Prescription { get; set; } = new();
    public virtual Patient Patient { get; set; } = new();
}
