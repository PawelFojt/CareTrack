using CareTrack.Server.Modules.Application.Prescription.Command;
using CareTrack.Server.Modules.Application.Prescription.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CareTrack.Server.Modules.Presentation.Controllers;

[ApiController]
[Route("prescription")]
public class PrescriptionController : CommonController
{
    private readonly IMediator _mediator;
    public PrescriptionController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("{prescriptionId:int}/add-medicine/{medicineId:int}")]
    public async Task<IActionResult> AddMedicineToPrescription(int prescriptionId, int medicineId)
    {
        if (prescriptionId == 0 || medicineId == 0) return WrongInputArgument();
        var command = new AddMedicineToPrescriptionCommand
        {
            MedicineId = medicineId,
            PrescriptionId = prescriptionId
            
        };
        var result = await _mediator.Send(command);
        return ConvertResult(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddPrescriptionCommand command)
    {
        var result = await _mediator.Send(command);
        return ConvertResult(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> List()
    {
        var result = await _mediator.Send(new GetPrescriptionsWithMedicinesQuery());
        return ConvertResult(result);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        if (id == 0) return WrongInputArgument();
        var result = await _mediator.Send(new GetPrescriptionWithMedicinesQuery { Id = id });
        return ConvertResult(result);
    }
    
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePrescriptionCommand command)
    {
        var result = await _mediator.Send(command);
        return ConvertResult(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id == 0) return WrongInputArgument();
        var result = await _mediator.Send(new DeletePrescriptionCommand { Id = id });
        return ConvertResult(result);
    }
}