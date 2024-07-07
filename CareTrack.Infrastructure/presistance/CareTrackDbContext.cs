using CareTrack.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace CareTrack.Infrastructure.presistance;
public class CareTrackDbContext(DbContextOptions<CareTrackDbContext> options) : DbContext(options)
{
    public DbSet<Medicine> Medicines { get; set; }
    public DbSet<RecipeMedicine> RecipeMedicines { get; set; }
    public DbSet<Recipe> Recipes { get; set; } 
    public DbSet<PatientRecipe> PatientRecipes { get; set; }
    public DbSet<Patient> Patients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        modelBuilder.Entity<RecipeMedicine>(entity =>
        {
            entity.HasKey(e => new
            {
                e.RecipeId,
                e.MedicineId
            });

            entity
                .HasOne(e => e.Recipe)
                .WithMany(e => e.RecipeMedicines)
                .HasForeignKey(e => e.RecipeId);
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        modelBuilder.Entity<PatientRecipe>(entity =>
        {
            entity.HasKey(e => new
            {
                e.PatientId,
                e.RecipeId
            });

            entity
                .HasOne(e => e.Patient)
                .WithMany(e => e.PatientRecipes)
                .HasForeignKey(e =>e.PatientId);
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        

        base.OnModelCreating(modelBuilder);
    }
}
