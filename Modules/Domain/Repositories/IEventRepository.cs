using CareTrack.Server.Modules.Domain.Models;

namespace CareTrack.Server.Modules.Domain.Repositories;

public interface IEventRepository
{
    Task<Result<IEnumerable<IEvent>>> List();
    Task<Result<IEnumerable<IEvent>>> ListByPatientId(int patientId);
    Task<Result<IEvent>> Get(int id);
    Task<Result<IEvent>> Add(IEvent e);
    Task<Result<IEvent>> Update(IEvent e);
    Task<Result<IEvent>> Delete(int id);
}