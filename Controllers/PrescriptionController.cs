using CareTrack.Server.Prescription.Command;
using CareTrack.Server.Prescription.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CareTrack.Server.Controllers;

[ApiController]
[Route("prescription")]
public class PrescriptionController(IMediator mediator) : CommonController
{
    [HttpPost("{prescriptionId:int}/add-medicine/{medicineId:int}")]
    public async Task<IActionResult> AddMedicineToPrescription(int prescriptionId, int medicineId)
    {
        if (prescriptionId == 0 || medicineId == 0) return WrongInputArgument();
        var command = new AddMedicineToPrescriptionCommand
        {
            MedicineId = medicineId,
            PrescriptionId = prescriptionId
            
        };
        var result = await mediator.Send(command);
        return ConvertResult(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] AddPrescriptionCommand command)
    {
        var result = await mediator.Send(command);
        return ConvertResult(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var result = await mediator.Send(new GetPrescriptionsWithMedicinesQuery());
        return ConvertResult(result);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        if (id == 0) return WrongInputArgument();
        var result = await mediator.Send(new GetPrescriptionWithMedicinesQuery { Id = id });
        return ConvertResult(result);
    }
    
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePrescriptionCommand command)
    {
        var result = await mediator.Send(command);
        return ConvertResult(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id == 0) return WrongInputArgument();
        var result = await mediator.Send(new DeletePrescriptionCommand { Id = id });
        return ConvertResult(result);
    }
}