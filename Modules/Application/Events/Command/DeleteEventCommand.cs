using CareTrack.Server.Modules.Domain.Models;
using CareTrack.Server.Modules.Domain.Repositories;
using MediatR;

namespace CareTrack.Server.Modules.Application.Events.Command;

public class DeleteEventCommand : IRequest<Result<IEvent>>
{
    public int Id { get; init; }
}

public class DeleteEventCommandHandler
    : IRequestHandler<DeleteEventCommand, Result<IEvent>>
{
    private readonly IEventRepository eventRepository;
    public DeleteEventCommandHandler(IEventRepository eventRepository)
    {
        this.eventRepository = eventRepository;
    }
    public async Task<Result<IEvent>> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var e = await eventRepository.Delete(request.Id);
        return e;
    }
}