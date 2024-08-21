using System.Net;
using CareTrack.Server.Entities;
using CareTrack.Server.Models;
using CareTrack.Server.presistance;
using Microsoft.EntityFrameworkCore;

namespace CareTrack.Server.Repositories;


public class PrescriptionRepository(CareTrackDbContext context) : IPrescriptionRepository
{

    public async Task<Result<IPrescription>> AddMedicineToPrescription(int prescriptionId, int medicineId)
    {
        var prescription = await context.Prescriptions
            .Include(r => r.PrescriptionMedicines)
                .ThenInclude(rm => rm.Medicine)
            .FirstOrDefaultAsync(r => r.Id == prescriptionId);

        var medicine = await context.Medicines.FindAsync(medicineId);

        if (prescription == null || medicine == null)
        {
            return Result<IPrescription>.Error("Nie znaleziono przepisu lub leku", HttpStatusCode.NotFound);
        }

        var existingLink = await context.PrescriptionMedicines
            .FirstOrDefaultAsync(rm => rm.PrescriptionId == prescriptionId && rm.MedicineId == medicineId);

        if (existingLink != null)
        {
            return new(prescription); 
        }

        var prescriptionMedicine = new PrescriptionMedicine
        {
            PrescriptionId = prescriptionId,
            MedicineId = medicineId
        };

        context.PrescriptionMedicines.Add(prescriptionMedicine);
        await context.SaveChangesAsync();

        prescription = await context.Prescriptions
            .Include(r => r.PrescriptionMedicines)
                .ThenInclude(rm => rm.Medicine)
            .FirstOrDefaultAsync(r => r.Id == prescriptionId);
        
        if (prescription == null)
        {
            return Result<IPrescription>.Error("Nie znaleziono recepty", HttpStatusCode.NotFound);
        }

        return new(prescription);
    }
    public async Task<Result<IEnumerable<IPrescriptionWithMedicines>>> List()
    {
        var prescriptions =
            await context.Prescriptions
                .Include(r => r.PrescriptionMedicines)
                    .ThenInclude(rm => rm.Medicine)
                .AsNoTracking()
                .ToListAsync();
        var prescriptionWithMedicines = prescriptions.Select(r => new PrescriptionWithMedicines()
        {
            Id = r.Id,
            Quantity = r.Quantity,
            DosingTime = r.DosingTime,
            Medicines = r.PrescriptionMedicines.Select(rm => new Models.Medicine()
            {
                Id = rm.Medicine.Id,
                Name = rm.Medicine.Name,
                Quantity = rm.Medicine.Quantity,
                ExpirationDate = rm.Medicine.ExpirationDate
            })
        });

        return new Result<IEnumerable<IPrescriptionWithMedicines>>(prescriptionWithMedicines);
    }
    
    public async Task<Result<IPrescriptionWithMedicines>> Get(int id)
    {
        var prescription = await context.Prescriptions
            .Include(r => r.PrescriptionMedicines)
                .ThenInclude(rm => rm.Medicine)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (prescription == null)
        {
            return Result<IPrescriptionWithMedicines>.Error("Nie znaleziono recepty", HttpStatusCode.NotFound);
        }

        var prescriptionWithMedicines = new PrescriptionWithMedicines()
        {
            Id = prescription.Id,
            Quantity = prescription.Quantity,
            DosingTime = prescription.DosingTime,
            Medicines = prescription.PrescriptionMedicines.Select(rm => new Models.Medicine()
            {
                Id = rm.Medicine.Id,
                Name = rm.Medicine.Name,
                Quantity = rm.Medicine.Quantity,
                ExpirationDate = rm.Medicine.ExpirationDate
            })
        };

        return new Result<IPrescriptionWithMedicines>(prescriptionWithMedicines);
    }

    public async Task<Result<IEnumerable<IPrescriptionWithMedicines>>> ListByIds(IEnumerable<int> ids)
    {
        var prescriptions = await context.Prescriptions
            .Include(r => r.PrescriptionMedicines)
            .ThenInclude(rm => rm.Medicine)
            .Where(r => ids.Contains(r.Id))
            .ToListAsync();
        if (prescriptions.Count == 0)
        {
            return Result<IEnumerable<IPrescriptionWithMedicines>>.Error("Nie znaleziono recept", HttpStatusCode.NotFound);
        }

        var prescriptionWithMedicines = prescriptions.Select(r => new PrescriptionWithMedicines()
        {
            Id = r.Id,
            Quantity = r.Quantity,
            DosingTime = r.DosingTime,
            Medicines = r.PrescriptionMedicines.Select(rm => new Models.Medicine()
            {
                Id = rm.Medicine.Id,
                Name = rm.Medicine.Name,
                Quantity = rm.Medicine.Quantity,
                ExpirationDate = rm.Medicine.ExpirationDate
            })
        });

        return new Result<IEnumerable<IPrescriptionWithMedicines>>(prescriptionWithMedicines);
    }
    
    public async Task<Result<IPrescription>> Add(IPrescription prescription)
    {
        var prescriptionToAdd = new Entities.Prescription
        {
            Quantity = prescription.Quantity,
            DosingTime = prescription.DosingTime,
        };
        var added = context.Prescriptions
            .Add(prescriptionToAdd);
        await context.SaveChangesAsync();
        return new Result<IPrescription>(added.Entity);
    }

    public async Task<Result<IPrescription>> Update(IPrescription prescription)
    {
        var prescriptionToUpdate = await context.Prescriptions.FindAsync(prescription.Id);

        if (prescriptionToUpdate == null) return Result<IPrescription>.Error("Brak recepty w bazie danych", HttpStatusCode.NotFound);

        prescriptionToUpdate.Quantity = prescription.Quantity;
        prescriptionToUpdate.DosingTime = prescription.DosingTime;
        
        var updated = context.Prescriptions
            .Update(prescriptionToUpdate);
        
        await context.SaveChangesAsync();
        
        return new Result<IPrescription>(updated.Entity);
    }

    public async Task<Result<IPrescription>> Delete(int id)
    {
        var prescription = await context.Prescriptions.FindAsync(id);
        
        var prescriptionMedicines = await context.PrescriptionMedicines
            .Where(rm => rm.PrescriptionId == id)
            .ToListAsync();

        if (prescription == null) return Result<IPrescription>.Error("Brak recepty w bazie danych", HttpStatusCode.NotFound);

        context.Prescriptions.Remove(prescription);
        
        if (prescriptionMedicines.Count != 0)
        {
            context.PrescriptionMedicines.RemoveRange(prescriptionMedicines);
        }
        
        await context.SaveChangesAsync();
        
        return new Result<IPrescription>(prescription);
    }
    
}