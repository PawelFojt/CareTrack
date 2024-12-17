using System.Net;
using CareTrack.Server.Modules.Domain.Models;
using CareTrack.Server.Modules.Domain.Repositories;
using MediatR;

namespace CareTrack.Server.Modules.Application.Patient.Queries;

public class GetPatientsQuery : IRequest<Result<IEnumerable<IPatient>>>
{
}

public class GetPatientsQueryHandler(IPatientRepository patientRepository) :
    IRequestHandler<GetPatientsQuery, Result<IEnumerable<IPatient>>>
{
    public async Task<Result<IEnumerable<IPatient>>> Handle(
        GetPatientsQuery request,
        CancellationToken cancellationToken)
    {
        var patients = await patientRepository.List();
        if (patients.IsError)
        {
            return Result<IEnumerable<IPatient>>.Error("Błąd pobierania listy pacjentów", HttpStatusCode.NotFound);
        }

        return patients;
    }
}