namespace CareTrack.Domain.Models;
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
