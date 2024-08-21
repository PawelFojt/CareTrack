namespace CareTrack.Server.Models;

public interface IPrescription
{
    int Id { get; set; }
    int Quantity { get; set; }
    List<TimeOnly> DosingTime { get; set; }
}

public interface IPrescriptionWithMedicines : IPrescription
{
    IEnumerable<IMedicine> Medicines { get; set; }
}

public class Prescription : IPrescription
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public List<TimeOnly> DosingTime { get; set; } = [];
}

public class PrescriptionWithMedicines : Prescription, IPrescriptionWithMedicines
{
    public IEnumerable<IMedicine> Medicines { get; set; } = [];
}
