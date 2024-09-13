using CareTrack.Server.Modules.Domain.Models;
using CareTrack.Server.Modules.Domain.Repositories;
using CareTrack.Server.Modules.Infrastructure.Entities;
using MediatR;

namespace CareTrack.Server.Modules.Application.Events.Command;

public class UpdateEventCommand : IRequest<Result<IEvent>>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime? Date { get; set; }
    public int PatientId { get; set; }
}

public class UpdateEventCommandHandler
    : IRequestHandler<UpdateEventCommand, Result<IEvent>>
{
    private readonly IEventRepository eventRepository;
    public UpdateEventCommandHandler(IEventRepository eventRepository)
    {
        this.eventRepository = eventRepository;
    }
    public async Task<Result<IEvent>> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        var e = new Event
        {
            Name = request.Name,
            Description = request.Description,
            Date = request.Date,
            PatientId = request.PatientId
        };
        var eventResult = await eventRepository.Update(e);
        return eventResult;
    }
}