using CareTrack.Server.Modules.Domain.Models;
using CareTrack.Server.Modules.Domain.Repositories;
using MediatR;

namespace CareTrack.Server.Modules.Application.Patient.Command;

public class UpdatePatientCommand : IRequest<Result<IPatient>>
{
    public required PatientResult Patient { get; init; }
}

public class UpdatePatientCommandHandler(IPatientRepository patientRepository)
    : IRequestHandler<UpdatePatientCommand, Result<IPatient>>
{
    public async Task<Result<IPatient>> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await patientRepository.Update(request.Patient);
        return patient;
    }
}