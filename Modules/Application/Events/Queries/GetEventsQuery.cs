using System.Net;
using CareTrack.Server.Modules.Domain.Models;
using CareTrack.Server.Modules.Domain.Repositories;
using MediatR;

namespace CareTrack.Server.Modules.Application.Events.Queries;

public class GetEventsQuery : IRequest<Result<IEnumerable<IEvent>>>
{
}

public class GetEventsQueryHandler(IEventRepository eventRepository)
    : IRequestHandler<GetEventsQuery, Result<IEnumerable<IEvent>>>
{
    public async Task<Result<IEnumerable<IEvent>>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
    {
        var events = await eventRepository.List();
        if (events.IsError || events.Value is null) 
            return Result<IEnumerable<IEvent>>.Error("Error while fetching events", HttpStatusCode.InternalServerError);
        return new (events.Value);
    }
}