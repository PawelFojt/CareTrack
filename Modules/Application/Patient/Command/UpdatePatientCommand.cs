using CareTrack.Server.Modules.Domain.Models;
using CareTrack.Server.Modules.Domain.Repositories;
using MediatR;

namespace CareTrack.Server.Modules.Application.Patient.Command;

public class UpdatePatientCommand : IRequest<Result<IPatient>>
{
    public PatientResult PatientResult { get; init; }
}

public class UpdatePatientCommandHandler
    : IRequestHandler<UpdatePatientCommand, Result<IPatient>>
{
    private readonly IPatientRepository patientRepository;
    public UpdatePatientCommandHandler(IPatientRepository patientRepository)
    {
        this.patientRepository = patientRepository;
    }
    public async Task<Result<IPatient>> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await patientRepository.Update(request.PatientResult);
        return patient;
    }
}