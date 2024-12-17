using CareTrack.Server.Modules.Domain.Models;
using CareTrack.Server.Modules.Domain.Repositories;
using MediatR;

namespace CareTrack.Server.Modules.Application.Patient.Command;

public class AddPrescriptionToPatientCommand : IRequest<Result<IPatientWithPrescriptions>>
{
    public int PrescriptionId { get; init; }
    public int PatientId { get; init; }
}

public class AddPrescriptionToPatientCommandHandler(IPatientRepository patientRepository)
    : IRequestHandler<AddPrescriptionToPatientCommand, Result<IPatientWithPrescriptions>>
{
    public async Task<Result<IPatientWithPrescriptions>> Handle(AddPrescriptionToPatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await patientRepository.AddPrescriptionToPatient(request.PatientId, request.PrescriptionId);
        return patient;
    }
}