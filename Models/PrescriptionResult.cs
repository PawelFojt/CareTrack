namespace CareTrack.Server.Models;

public interface IPrescription
{
    int Id { get; set; }
    int Quantity { get; set; }
    List<TimeOnly> DosingTime { get; set; }
}

public interface IPrescriptionWithMedicines
{
    int Id { get; set; }
    int Quantity { get; set; }
    List<TimeOnly> DosingTime { get; set; }
    IEnumerable<IMedicine> Medicines { get; set; }
}

public class PrescriptionResult : IPrescription
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public List<TimeOnly> DosingTime { get; set; } = new();
}

public class PrescriptionResultWithMedicines : PrescriptionResult, IPrescriptionWithMedicines
{
    public IEnumerable<IMedicine> Medicines { get; set; } = new List<IMedicine>();
}
