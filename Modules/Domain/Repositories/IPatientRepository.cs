using CareTrack.Server.Modules.Domain.Models;

namespace CareTrack.Server.Modules.Domain.Repositories;

public interface IPatientRepository
{
    Task<Result<IPatientWithPrescriptions>> AddPrescriptionToPatient(int patientId, int prescriptionId);
    Task<Result<IPatientWithPrescriptions>> Get(int id);
    Task<Result<IEnumerable<IPatient>>> List();
    Task<Result<IPatient>> Add(IPatient patient);
    Task<Result<IPatient>> Update(IPatient patient);
    Task<Result<IPatient>> Delete(int id);
}