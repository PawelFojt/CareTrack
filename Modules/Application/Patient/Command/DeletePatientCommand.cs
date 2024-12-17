using CareTrack.Server.Modules.Domain.Models;
using CareTrack.Server.Modules.Domain.Repositories;
using MediatR;

namespace CareTrack.Server.Modules.Application.Patient.Command;

public class DeletePatientCommand : IRequest<Result<IPatient>>
{
    public  int Id { get; init; }
}

public class DeletePatientCommandHandler(IPatientRepository patientRepository) :
    IRequestHandler<DeletePatientCommand, Result<IPatient>>
{
    public async Task<Result<IPatient>> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await patientRepository.Delete(request.Id);
        return patient;
    }
}