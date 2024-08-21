using CareTrack.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace CareTrack.Server.presistance;
public class CareTrackDbContext : DbContext
{
    public CareTrackDbContext(DbContextOptions<CareTrackDbContext> options) : base(options)
    {
        
    }
    public DbSet<Entities.Medicine> Medicines { get; set; }
    public DbSet<PrescriptionMedicine> PrescriptionMedicines { get; set; }
    public DbSet<Entities.Prescription> Prescriptions { get; set; } 
    public DbSet<PatientPrescription> PatientPrescriptions { get; set; }
    public DbSet<Patient> Patients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entities.Medicine>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        modelBuilder.Entity<PrescriptionMedicine>(entity =>
        {
            entity.HasKey(e => new
            {
                e.PrescriptionId,
                e.MedicineId
            });

            entity
                .HasOne(e => e.Prescription)
                .WithMany(e => e.PrescriptionMedicines)
                .HasForeignKey(e => e.PrescriptionId);
        });

        modelBuilder.Entity<Entities.Prescription>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        modelBuilder.Entity<PatientPrescription>(entity =>
        {
            entity.HasKey(e => new
            {
                e.PatientId, e.PrescriptionId
            });

            entity
                .HasOne(e => e.Patient)
                .WithMany(e => e.PatientPrescriptions)
                .HasForeignKey(e =>e.PatientId);
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        

        base.OnModelCreating(modelBuilder);
    }
}
