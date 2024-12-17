using CareTrack.Server.Modules.Application.Patient.Command;
using CareTrack.Server.Modules.Application.Patient.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CareTrack.Server.Modules.Presentation.Controllers;

[ApiController]
[Route("patient")]
public class PatientController : CommonController
{
    private readonly IMediator _mediator;

    public PatientController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("{patientId:int}/add-prescription/{prescriptionId:int}")]
    public async Task<IActionResult> AddPrescriptionToPatient(int patientId, int prescriptionId)
    {
        if (patientId == 0 || prescriptionId == 0) return WrongInputArgument();
        var command = new AddPrescriptionToPatientCommand
        {
            PrescriptionId = prescriptionId,
            PatientId = patientId
            
        };
        var result = await _mediator.Send(command);
        return ConvertResult(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddPatientCommand command)
    {
        var result = await _mediator.Send(command);
        return ConvertResult(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> List()
    {
        var result = await _mediator.Send(new GetPatientsQuery());
        return ConvertResult(result);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        if (id == 0) return WrongInputArgument();
        var result = await _mediator.Send(new GetPatientWithPrescriptionsQuery { Id = id });
        return ConvertResult(result);
    }
    
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePatientCommand command)
    {
        var result = await _mediator.Send(command);
        return ConvertResult(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id == 0) return WrongInputArgument();
        var result = await _mediator.Send(new DeletePatientCommand { Id = id });
        return ConvertResult(result);
    }
}