﻿
namespace CareTrack.Server.Entities;
public class PrescriptionMedicine
{
    public int MedicineId { get; set; }
    public int PrescriptionId { get; set; }
    
    public Medicine? Medicine { get; set; }
    public Prescription? Prescription { get; set; }
}