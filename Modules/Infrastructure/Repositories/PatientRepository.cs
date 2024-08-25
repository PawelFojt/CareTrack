using System.Net;
using CareTrack.Server.Modules.Domain.Models;
using CareTrack.Server.Modules.Domain.Repositories;
using CareTrack.Server.Modules.Infrastructure.Entities;
using CareTrack.Server.Modules.Infrastructure.presistance;
using Microsoft.EntityFrameworkCore;
using Patient = CareTrack.Server.Modules.Infrastructure.Entities.Patient;

namespace CareTrack.Server.Modules.Infrastructure.Repositories;


public class PatientRepository : IPatientRepository
{
    private readonly CareTrackDbContext context;
    private readonly IPrescriptionRepository prescriptionRepository;

    public PatientRepository(CareTrackDbContext context, IPrescriptionRepository prescriptionRepository)
    {
        this.context = context;
        this.prescriptionRepository = prescriptionRepository;
    }
    public async Task<Result<IPatientWithPrescriptions>> AddPrescriptionToPatient(int patientId, int prescriptionId)
    {
        var patient = await context.Patients
            .Include(p => p.PatientPrescriptions)
            .FirstOrDefaultAsync(p => p.Id == patientId);

        var prescription = await context.Prescriptions.FindAsync(prescriptionId);

        if (patient == null || prescription == null)
        {
            return Result<IPatientWithPrescriptions>.Error("Nie znaleziono pacjenta lub recepty", HttpStatusCode.NotFound);
        }

        var existingLink = await context.PatientPrescriptions
            .FirstOrDefaultAsync(pp => pp.PatientId == patientId && pp.PrescriptionId == prescriptionId);

        if (existingLink != null)
        {
            return Result<IPatientWithPrescriptions>.Error("Pacjent już ma tę receptę", HttpStatusCode.BadRequest);
        }

        var patientPrescription = new PatientPrescription()
        {
            PatientId = patientId,
            PrescriptionId = prescriptionId
        };

        context.PatientPrescriptions.Add(patientPrescription);
        await context.SaveChangesAsync();

        var patientWithPrescriptions = await Get(patientId);

        if (patientWithPrescriptions.IsError || patientWithPrescriptions.Value == null)
        {
            return Result<IPatientWithPrescriptions>.Error("Nie znaleziono pacjenta", HttpStatusCode.NotFound);
        }

        return new Result<IPatientWithPrescriptions>(patientWithPrescriptions.Value);
    }
    
    public async Task<Result<IPatientWithPrescriptions>> Get(int id)
    {
        var patient = await context.Patients
            .FirstOrDefaultAsync(p => p.Id == id);
        
        if (patient == null)
        {
            return Result<IPatientWithPrescriptions>.Error("Nie znaleziono pacjenta", HttpStatusCode.NotFound);
        }
        
        var patientPrescriptionsId = await context.PatientPrescriptions
            .Where(pp => pp.PatientId == id)
            .Select(pp => pp.PrescriptionId)
            .ToListAsync();

        var prescriptionsWithMedicines = await prescriptionRepository.ListByIds(patientPrescriptionsId);
        
        var patientWithPrescriptions = new PatientWithPrescriptions()
        {
            Id = patient.Id,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Age = patient.Age,
            Weight = patient.Weight,
            PhoneNumber = patient.PhoneNumber,
            Admission = patient.Admission,
            Discharge = patient.Discharge,
            PrescriptionsWithMedicines = prescriptionsWithMedicines.Value
        };
        
        return new Result<IPatientWithPrescriptions>(patientWithPrescriptions);
    }
    
    public async Task<Result<IEnumerable<IPatient>>> List()
    {
        var result =
            await context.Patients
                .AsNoTracking()
                .Select(patient => new PatientResult()
                {
                    Id = patient.Id,
                    Admission = patient.Admission,
                    Age = patient.Age,
                    Discharge = patient.Discharge,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    PhoneNumber = patient.PhoneNumber,
                    Weight = patient.Weight
                })
                .ToListAsync();
        
        return new(result);
    }
    
    public async Task<Result<IPatient>> Add(IPatient patient)
    {
        var patientToAdd = new Patient()
        {
            Admission = patient.Admission,
            Age = patient.Age,
            Discharge = patient.Discharge,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            PhoneNumber = patient.PhoneNumber,
            Weight = patient.Weight
        };
        var added = context.Patients
            .Add(patientToAdd);
        await context.SaveChangesAsync();
        return new Result<IPatient>(added.Entity);
    }
    
    public async Task<Result<IPatient>> Update(IPatient patient)
    {
        var patientToUpdate = await context.Patients.FindAsync(patient.Id);
        
        if (patientToUpdate == null) return Result<IPatient>.Error("Brak pacjenta w bazie danych", HttpStatusCode.NotFound);
        
        patientToUpdate.FirstName = patient.FirstName;
        patientToUpdate.LastName = patient.LastName;
        patientToUpdate.Age = patient.Age;
        patientToUpdate.Weight = patient.Weight;
        patientToUpdate.PhoneNumber = patient.PhoneNumber;
        patientToUpdate.Admission = patient.Admission;
        patientToUpdate.Discharge = patient.Discharge;
        
        var updated = context.Patients
            .Update(patientToUpdate);
        
        await context.SaveChangesAsync();
        
        return new Result<IPatient>(updated.Entity);
    }
    
    public async Task<Result<IPatient>> Delete(int id)
    {
        var patientToDelete = await context.Patients.FindAsync(id);
        
        if (patientToDelete == null) return Result<IPatient>.Error("Brak pacjenta w bazie danych", HttpStatusCode.NotFound);
        
        context.Patients.Remove(patientToDelete);
        await context.SaveChangesAsync();
        
        return Result<IPatient>.Info("Pomyślnie usunięto");
    }
    
}