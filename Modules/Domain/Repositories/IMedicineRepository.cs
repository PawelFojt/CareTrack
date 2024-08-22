using CareTrack.Server.Modules.Domain.Models;

namespace CareTrack.Server.Modules.Domain.Repositories;

public interface IMedicineRepository
{
    Task<Result<IEnumerable<IMedicine>>> GetList();
    Task<Result<IMedicine>> Add(IMedicine medicine);
    Task<Result<IMedicine>> Update(IMedicine medicine);
    Task<Result<IMedicine>> Delete(int id);
}