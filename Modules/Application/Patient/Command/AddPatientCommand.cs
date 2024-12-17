using CareTrack.Server.Modules.Domain.Models;
using CareTrack.Server.Modules.Domain.Repositories;
using MediatR;

namespace CareTrack.Server.Modules.Application.Patient.Command;

public class AddPatientCommand : IRequest<Result<IPatient>>
{
    public required PatientResult Patient { get; init; }
}

public class AddPatientCommandHandler(IPatientRepository patientRepository)
    : IRequestHandler<AddPatientCommand, Result<IPatient>>
{
    public async Task<Result<IPatient>> Handle(AddPatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await patientRepository.Add(request.Patient);
        return patient;
    }
}