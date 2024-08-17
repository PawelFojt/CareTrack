using CareTrack.Application.Medicine.Command;
using CareTrack.Application.Medicine.Queries;
using CareTrack.Domain.Models;
using CareTrack.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Medicine = CareTrack.Infrastructure.Entities.Medicine;

namespace CareTrack.Server.Controllers;

[ApiController]
[Route("Medicine")]
public class MedicineController(IMediator mediator) : CommonController(mediator)
{
    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var getMedicinesQuery = new GetMedicinesQuery();
        var result = await mediator.Send(getMedicinesQuery);
        return ConvertResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody]Medicine? medicine)
    {
        if (medicine is null) return WrongInputArgument();
        var addMedicineCommand = new AddMedicineCommand()
        {
            Medicine = medicine
        };

        var result = await mediator.Send(addMedicineCommand);
        return ConvertResult(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody]Medicine? medicine)
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