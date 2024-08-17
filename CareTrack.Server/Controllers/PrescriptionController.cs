using CareTrack.Application.Prescription.Command;
using CareTrack.Application.Prescription.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CareTrack.Server.Controllers;

[ApiController]
[Route("prescription")]
public class PrescriptionController(IMediator mediator) : CommonController(mediator)
{
    [HttpPost("{prescriptionId:int}/add-medicine/{medicineId:int}")]
    public async Task<IActionResult> AddMedicineToPrescription(int prescriptionId, int medicineId)
    {
        var command = new AddMedicineToPrescriptionCommand
        {
            MedicineId = medicineId,
            PrescriptionId = prescriptionId
        };
        var result = await mediator.Send(command);
        return ConvertResult(result);
    }
    
    [HttpPost("add")]
    public async Task<IActionResult> AddPrescription([FromBody] AddPrescriptionCommand command)
    {
        var result = await mediator.Send(command);
        return ConvertResult(result);
    }
    
    [HttpGet("list")]
    public async Task<IActionResult> GetList()
    {
        var result = await mediator.Send(new GetPrescriptionsWithMedicinesQuery());
        return ConvertResult(result);
    }

}