using CareTrack.Server.Modules.Application.Medicine.Command;
using CareTrack.Server.Modules.Application.Medicine.Queries;
using CareTrack.Server.Modules.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CareTrack.Server.Modules.Presentation.Controllers;

[ApiController]
[Route("medicine")]
public class MedicineController : CommonController
{
    private readonly IMediator mediator;
    public MedicineController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    [HttpGet]
    public async Task<IActionResult> List()
    {
        var getMedicinesQuery = new GetMedicinesQuery();
        var result = await mediator.Send(getMedicinesQuery);
        return ConvertResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody]AddMedicineCommand command)
    {
        var result = await mediator.Send(command);
        return ConvertResult(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody]MedicineResult? medicine)
    {
        if (medicine is null) return WrongInputArgument();
        var updateMedicineCommand = new UpdateMedicineCommand()
        {
            Medicine = medicine
        };
        var result = await mediator.Send(updateMedicineCommand);
        return ConvertResult(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id == 0) return WrongInputArgument();
        var deleteMedicineCommand = new DeleteMedicineCommand()
        {
            Id = id
        };
        var result = await mediator.Send(deleteMedicineCommand);
        return ConvertResult(result);
    }
}