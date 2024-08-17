using CareTrack.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace CareTrack.Infrastructure.presistance;
public class CareTrackDbContext(DbContextOptions<CareTrackDbContext> options) : DbContext(options)
{
    public DbSet<Medicine> Medicines { get; set; }
    public DbSet<PrescriptionMedicine> PrescriptionMedicines { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; } 
    public DbSet<PatientPrescription> PatientPrescriptions { get; set; }
    public DbSet<Patient> Patients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Medicine>(entity =>
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

        modelBuilder.Entity<Prescription>(entity =>
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
