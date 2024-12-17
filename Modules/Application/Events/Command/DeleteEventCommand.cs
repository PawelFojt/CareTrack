using CareTrack.Server.Modules.Domain.Models;
using CareTrack.Server.Modules.Domain.Repositories;
using MediatR;

namespace CareTrack.Server.Modules.Application.Events.Command;

public class DeleteEventCommand : IRequest<Result<IEvent>>
{
    public int Id { get; init; }
}

public class DeleteEventCommandHandler(IEventRepository eventRepository)
    : IRequestHandler<DeleteEventCommand, Result<IEvent>>
{
    public async Task<Result<IEvent>> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var e = await eventRepository.Delete(request.Id);
        return e;
    }
}