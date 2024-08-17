using System.Net;
using CareTrack.Domain.Models;
using CareTrack.Domain.Repositories;
using CareTrack.Infrastructure.Entities;
using CareTrack.Infrastructure.presistance;
using Microsoft.EntityFrameworkCore;
using Prescription = CareTrack.Infrastructure.Entities.Prescription;

namespace CareTrack.Infrastructure.Repositories;


public class PrescriptionRepository : IPrescriptionRepository
{
    private readonly CareTrackDbContext _context;

    public PrescriptionRepository(CareTrackDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IPrescription>> AddMedicineToPrescription(int prescriptionId, int medicineId)
    {
        var prescription = await _context.Prescriptions
            .Include(r => r.PrescriptionMedicines)
                .ThenInclude(rm => rm.Medicine)
            .FirstOrDefaultAsync(r => r.Id == prescriptionId);

        var medicine = await _context.Medicines.FindAsync(medicineId);

        if (prescription == null || medicine == null)
        {
            return Result<IPrescription>.Error("Nie znaleziono przepisu lub leku", HttpStatusCode.NotFound);
        }

        var existingLink = await _context.PrescriptionMedicines
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

        _context.PrescriptionMedicines.Add(prescriptionMedicine);
        await _context.SaveChangesAsync();

        prescription = await _context.Prescriptions
            .Include(r => r.PrescriptionMedicines)
                .ThenInclude(rm => rm.Medicine)
            .FirstOrDefaultAsync(r => r.Id == prescriptionId);
        
        if (prescription == null)
        {
            return Result<IPrescription>.Error("Nie znaleziono recepty", HttpStatusCode.NotFound);
        }

        return new(prescription);
    }
    public async Task<Result<IEnumerable<IPrescriptionWithMedicines>>> GetList()
    {
        var prescriptions =
            await _context.Prescriptions
                .Include(r => r.PrescriptionMedicines)
                    .ThenInclude(rm => rm.Medicine)
                .AsNoTracking()
                .ToListAsync();
        var prescriptionWithMedicines = prescriptions.Select(r => new Domain.Models.PrescriptionWithMedicines()
        {
            Id = r.Id,
            Quantity = r.Quantity,
            DosingTime = r.DosingTime,
            Medicines = r.PrescriptionMedicines.Select(rm => new Domain.Models.Medicine()
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
        var prescription = await _context.Prescriptions
            .Include(r => r.PrescriptionMedicines)
                .ThenInclude(rm => rm.Medicine)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (prescription == null)
        {
            return Result<IPrescriptionWithMedicines>.Error("Nie znaleziono recepty", HttpStatusCode.NotFound);
        }

        var prescriptionWithMedicines = new Domain.Models.PrescriptionWithMedicines()
        {
            Id = prescription.Id,
            Quantity = prescription.Quantity,
            DosingTime = prescription.DosingTime,
            Medicines = prescription.PrescriptionMedicines.Select(rm => new Domain.Models.Medicine()
            {
                Id = rm.Medicine.Id,
                Name = rm.Medicine.Name,
                Quantity = rm.Medicine.Quantity,
                ExpirationDate = rm.Medicine.ExpirationDate
            })
        };

        return new Result<IPrescriptionWithMedicines>(prescriptionWithMedicines);
    }
    
    public async Task<Result<IPrescription>> Add(IPrescription prescription)
    {
        var prescriptionToAdd = new Prescription
        {
            Quantity = prescription.Quantity,
            DosingTime = prescription.DosingTime,
        };
        var added = _context.Prescriptions
            .Add(prescriptionToAdd);
        await _context.SaveChangesAsync();
        return new Result<IPrescription>(added.Entity);
    }

    public async Task<Result<IPrescription>> Update(IPrescription prescription)
    {
        var prescriptionToUpdate = await _context.Prescriptions.FindAsync(prescription.Id);

        if (prescriptionToUpdate == null) return Result<IPrescription>.Error("Brak recepty w bazie danych", HttpStatusCode.NotFound);

        prescriptionToUpdate.Quantity = prescription.Quantity;
        prescriptionToUpdate.DosingTime = prescription.DosingTime;
        
        var updated = _context.Prescriptions
            .Update(prescriptionToUpdate);
        
        await _context.SaveChangesAsync();
        
        return new Result<IPrescription>(updated.Entity);
    }

    public async Task<Result<IPrescription>> Delete(int id)
    {
        var prescription = await _context.Prescriptions.FindAsync(id);
        
        var prescriptionMedicines = await _context.PrescriptionMedicines
            .Where(rm => rm.PrescriptionId == id)
            .ToListAsync();

        if (prescription == null) return Result<IPrescription>.Error("Brak recepty w bazie danych", HttpStatusCode.NotFound);

        _context.Prescriptions.Remove(prescription);
        
        if (prescriptionMedicines.Count != 0)
        {
            _context.PrescriptionMedicines.RemoveRange(prescriptionMedicines);
        }
        
        await _context.SaveChangesAsync();
        
        return new Result<IPrescription>(prescription);
    }
    
}