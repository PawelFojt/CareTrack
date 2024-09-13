namespace CareTrack.Server.Modules.Domain.Models;
public interface IPatient
{
    int Id { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; } 
    int Age { get; set; }
    int Weight { get; set; }
    string PhoneNumber { get; set; }
    DateTime Admission { get; set; }
    DateTime Discharge { get; set; }
}

public interface IPatientWithPrescriptions
{
    int Id { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; } 
    int Age { get; set; }
    int Weight { get; set; }
    string PhoneNumber { get; set; }
    DateTime Admission { get; set; }
    DateTime Discharge { get; set; }
    IEnumerable<IPrescriptionWithMedicines>? PrescriptionsWithMedicines { get; set; }
    IEnumerable<IEvent>? Events { get; set; }
}

public class PatientWithPrescriptions : IPatientWithPrescriptions
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
    public int Weight { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime Admission { get; set; }
    public DateTime Discharge { get; set; }
    public IEnumerable<IPrescriptionWithMedicines>? PrescriptionsWithMedicines { get; set; }
    public IEnumerable<IEvent>? Events { get; set; }
}

public class PatientResult : IPatient
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
    public int Weight { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime Admission { get; set; }
    public DateTime Discharge { get; set; }
}
