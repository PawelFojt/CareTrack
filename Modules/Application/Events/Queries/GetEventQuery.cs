using System.Net;
using CareTrack.Server.Modules.Domain.Models;
using CareTrack.Server.Modules.Domain.Repositories;
using MediatR;

namespace CareTrack.Server.Modules.Application.Events.Queries;

public class GetEventQuery : IRequest<Result<IEvent>>
{
    public int Id { get; init; }
}

public class GetEventQueryHandler
    : IRequestHandler<GetEventQuery, Result<IEvent>>
{
    private readonly IEventRepository eventRepository;
    public GetEventQueryHandler(IEventRepository eventRepository)
    {
        this.eventRepository = eventRepository;
    }
    public async Task<Result<IEvent>> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        var e = await eventRepository.Get(request.Id);
        if (e.IsError || e.Value is null) 
            return Result<IEvent>.Error("Error while fetching events", HttpStatusCode.InternalServerError);
        return new (e.Value);
    }
}