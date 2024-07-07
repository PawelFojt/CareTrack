using CareTrack.Domain.Models;
using CareTrack.Domain.Repositories;
using CareTrack.Infrastructure.presistance;
using Microsoft.EntityFrameworkCore;
using Medicine = CareTrack.Infrastructure.Entities.Medicine;

namespace CareTrack.Infrastructure.Repositories;

public class MedicineRepository : IMedicineRepository
{
    private readonly CareTrackDbContext _context;

    public MedicineRepository(CareTrackDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<IMedicine>>> GetList()
    {
        var result =
            await _context.Medicines
                .AsNoTracking()
                .Select(medicine => (IMedicine)medicine)
                .ToListAsync();
      

        return new Result<List<IMedicine>>(result);
    }
    public async Task<Result<IMedicine>> Add(IMedicine medicine)
    {
        var added = _context.Medicines
            .Add((Medicine)medicine);
        await _context.SaveChangesAsync();
        return new Result<IMedicine>(added.Entity);
    }

    public async Task<Result<IMedicine>> Update(IMedicine medicine)
    {
        var medicineToUpdate = await _context.Medicines.FindAsync(medicine.Id);

        if (medicineToUpdate == null) return Result<IMedicine>.Error("Brak leku w bazie danych");

        medicineToUpdate.Name = medicine.Name;
        medicineToUpdate.Quantity = medicine.Quantity;
        medicineToUpdate.ExpirationDate = medicine.ExpirationDate;
        
        var updated = _context.Medicines
            .Update(medicineToUpdate);
        
        await _context.SaveChangesAsync();
        
        return new Result<IMedicine>(updated.Entity);
    }

    public async Task<Result<IMedicine>> Delete(int id)
    {
        var medicineToDelete = await _context.Medicines.FindAsync(id);

        if (medicineToDelete == null) return Result<IMedicine>.Error("Brak leku w bazie danych");

        _context.Medicines.Remove(medicineToDelete);
        await _context.SaveChangesAsync();

        
        return Result<IMedicine>.Info("Pomyślnie usunięto");
    }
}

