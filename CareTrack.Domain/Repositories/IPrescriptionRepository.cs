using CareTrack.Domain.Models;

namespace CareTrack.Domain.Repositories;

public interface IPrescriptionRepository
{
    Task<Result<IPrescription>> AddMedicineToPrescription(int prescriptionId, int medicineId);
    Task<Result<IEnumerable<IPrescriptionWithMedicines>>> GetList();
    Task<Result<IPrescription>> Add(IPrescription prescription);
    Task<Result<IPrescription>> Update(IPrescription medicine);
    Task<Result<IMedicine>> Delete(int id);
}