using CareTrack.Server.Modules.Application.Events.Command;
using CareTrack.Server.Modules.Application.Events.Queries;
using CareTrack.Server.Modules.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CareTrack.Server.Modules.Presentation.Controllers;

[ApiController]
[Route("event")]
public class EventController : CommonController
{
    private readonly IMediator _mediator;
    public EventController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        if (id == 0) return WrongInputArgument();
        var getEventByIdQuery = new GetEventQuery()
        {
            Id = id
        };
        var result = await _mediator.Send(getEventByIdQuery);
        return ConvertResult(result);
    }
    [HttpGet]
    public async Task<IActionResult> List()
    {
        var getEventsQuery = new GetEventsQuery();
        var result = await _mediator.Send(getEventsQuery);
        return ConvertResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody]AddEventCommand command)
    {
        var result = await _mediator.Send(command);
        return ConvertResult(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody]UpdateEventCommand command)
    {
        var result = await _mediator.Send(command);
        return ConvertResult(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id == 0) return WrongInputArgument();
        var deleteEventCommand = new DeleteEventCommand()
        {
            Id = id
        };
        var result = await _mediator.Send(deleteEventCommand);
        return ConvertResult(result);
    }
}