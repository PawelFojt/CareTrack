using System.Net;
using CareTrack.Server.Models;
using CareTrack.Server.presistance;
using Microsoft.EntityFrameworkCore;

namespace CareTrack.Server.Repositories;

public class MedicineRepository : IMedicineRepository
{
    private readonly CareTrackDbContext context;

    public MedicineRepository(CareTrackDbContext context)
    {
        this.context = context;
    }

    public async Task<Result<IEnumerable<IMedicine>>> GetList()
    {
        var result =
            await context.Medicines
                .AsNoTracking()
                .Select(medicine => new MedicineResult()
                {
                    Id = medicine.Id,
                    Name = medicine.Name,
                    Quantity = medicine.Quantity,
                    ExpirationDate = medicine.ExpirationDate
                })
                .ToListAsync();
      

        return new (result);
    }
    public async Task<Result<IMedicine>> Add(IMedicine medicine)
    {
        var medicineToAdd = new Entities.Medicine()
        {
            Name = medicine.Name,
            Quantity = medicine.Quantity,
            ExpirationDate = medicine.ExpirationDate
        };
        var added = context.Medicines
            .Add(medicineToAdd);
        await context.SaveChangesAsync();
        return new Result<IMedicine>(added.Entity);
    }

    public async Task<Result<IMedicine>> Update(IMedicine medicine)
    {
        var medicineToUpdate = await context.Medicines.FindAsync(medicine.Id);

        if (medicineToUpdate == null) return Result<IMedicine>.Error("Brak leku w bazie danych", HttpStatusCode.NotFound);

        medicineToUpdate.Name = medicine.Name;
        medicineToUpdate.Quantity = medicine.Quantity;
        medicineToUpdate.ExpirationDate = medicine.ExpirationDate;
        
        var updated = context.Medicines
            .Update(medicineToUpdate);
        
        await context.SaveChangesAsync();
        
        return new Result<IMedicine>(updated.Entity);
    }

    public async Task<Result<IMedicine>> Delete(int id)
    {
        var medicineToDelete = await context.Medicines.FindAsync(id);

        if (medicineToDelete == null) return Result<IMedicine>.Error("Brak leku w bazie danych", HttpStatusCode.NotFound);

        context.Medicines.Remove(medicineToDelete);
        await context.SaveChangesAsync();

        
        return Result<IMedicine>.Info("Pomyślnie usunięto");
    }
}

