﻿
using CareTrack.Domain.Models;

namespace CareTrack.Infrastructure.Entities;
public class PatientPrescription 
{
    public int PrescriptionId { get; set; }
    public int PatientId { get; set; }
    public virtual Prescription? Prescription { get; set; }
    public virtual Patient? Patient { get; set; }
}
