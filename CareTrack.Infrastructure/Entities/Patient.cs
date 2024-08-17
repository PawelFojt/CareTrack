﻿using CareTrack.Domain.Models;

namespace CareTrack.Infrastructure.Entities;
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
    public virtual ICollection<PatientPrescription>? PatientPrescriptions { get; set; }
}
