using CareTrack.Server.Modules.Domain.Models;
using CareTrack.Server.Modules.Domain.Repositories;
using CareTrack.Server.Modules.Infrastructure.Entities;
using MediatR;

namespace CareTrack.Server.Modules.Application.Events.Command;

public class AddEventCommand : IRequest<Result<IEvent>>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime? Date { get; set; }
    public int PatientId { get; set; }
}

public class AddEventCommandHandler
    : IRequestHandler<AddEventCommand, Result<IEvent>>
{
    private readonly IEventRepository medicineRepository;
    public AddEventCommandHandler(IEventRepository medicineRepository)
    {
        this.medicineRepository = medicineRepository;
    }
    public async Task<Result<IEvent>> Handle(AddEventCommand request, CancellationToken cancellationToken)
    {
        var e = new Event
        {
            Name = request.Name,
            Description = request.Description,
            Date = request.Date,
            PatientId = request.PatientId
        };
        var eventResult = await medicineRepository.Add(e);
        return eventResult;
    }
}