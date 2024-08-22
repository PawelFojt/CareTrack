using CareTrack.Server.Modules.Domain.Models;

namespace CareTrack.Server.Modules.Domain.Repositories;

public interface IPrescriptionRepository
{
    Task<Result<IPrescription>> AddMedicineToPrescription(int prescriptionId, int medicineId);
    Task<Result<IEnumerable<IPrescriptionWithMedicines>>> List();
    Task<Result<IEnumerable<IPrescriptionWithMedicines>>> ListByIds(IEnumerable<int> ids);
    Task<Result<IPrescriptionWithMedicines>> Get(int id);
    Task<Result<IPrescription>> Add(IPrescription prescription);
    Task<Result<IPrescription>> Update(IPrescription medicine);
    Task<Result<IPrescription>> Delete(int id);
    
}