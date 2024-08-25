using CareTrack.Server.Modules.Domain.Models;
using CareTrack.Server.Modules.Domain.Repositories;
using MediatR;

namespace CareTrack.Server.Modules.Application.Patient.Command;

public class AddPatientCommand : IRequest<Result<IPatient>>
{
    public PatientResult PatientResult { get; set; }
}

public class AddPatientCommandHandler
    : IRequestHandler<AddPatientCommand, Result<IPatient>>
{
    private readonly IPatientRepository patientRepository;
    public AddPatientCommandHandler(IPatientRepository patientRepository)
    {
        this.patientRepository = patientRepository;
    }
    public async Task<Result<IPatient>> Handle(AddPatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await patientRepository.Add(request.PatientResult);
        return patient;
    }
}