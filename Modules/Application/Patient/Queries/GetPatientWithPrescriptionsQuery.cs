using CareTrack.Server.Modules.Domain.Models;
using CareTrack.Server.Modules.Domain.Repositories;
using MediatR;

namespace CareTrack.Server.Modules.Application.Patient.Queries;

public class GetPatientWithPrescriptionsQuery : IRequest<Result<IPatientWithPrescriptions>>
{
    public int Id { get; set; }
}

public class GetPatientWithPrescriptionsQueryHandler
    : IRequestHandler<GetPatientWithPrescriptionsQuery, Result<IPatientWithPrescriptions>>
{
    private readonly IPatientRepository patientRepository;
    public GetPatientWithPrescriptionsQueryHandler(IPatientRepository patientRepository)
    {
        this.patientRepository = patientRepository;
    }
    public async Task<Result<IPatientWithPrescriptions>> Handle(
        GetPatientWithPrescriptionsQuery request,
        CancellationToken cancellationToken)
    {
        var patient = await patientRepository.Get(request.Id);
        return patient;
    }
}