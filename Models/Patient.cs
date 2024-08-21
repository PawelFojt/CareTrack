namespace CareTrack.Server.Models;
public interface IPatient
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; } 
    public int Age { get; set; }
    public int Weight { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime Admission { get; set; }
    public DateTime Discharge { get; set; }
}

public interface IPatientWithPrescriptions : IPatient
{
    public IEnumerable<IPrescriptionWithMedicines> PrescriptionsWithMedicines { get; set; }
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
    public IEnumerable<IPrescriptionWithMedicines> PrescriptionsWithMedicines { get; set; } = new List<IPrescriptionWithMedicines>();
}

public class Patient : IPatient
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
