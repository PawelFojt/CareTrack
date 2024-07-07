using CareTrack.Domain.Models;
namespace CareTrack.Domain.Repositories;

public interface IMedicineRepository
{
    Task<Result<List<IMedicine>>> GetList();
    Task<Result<IMedicine>> Add(IMedicine medicine);
    Task<Result<IMedicine>> Update(IMedicine medicine);
    Task<Result<IMedicine>> Delete(int id);
}