using CareTrack.Server.Models;
using CareTrack.Server.Repositories;
using MediatR;

namespace CareTrack.Server.Prescription.Queries;

public class GetPrescriptionsWithMedicinesQuery : IRequest<Result<IEnumerable<IPrescriptionWithMedicines>>>
{
}

public class GetPrescriptionsWithMedicinesQueryHandler(IPrescriptionRepository prescriptionRepository)
    : IRequestHandler<GetPrescriptionsWithMedicinesQuery, Result<IEnumerable<IPrescriptionWithMedicines>>>
{
    public async Task<Result<IEnumerable<IPrescriptionWithMedicines>>> Handle(
        GetPrescriptionsWithMedicinesQuery request,
        CancellationToken cancellationToken)
    {
        var prescriptions = await prescriptionRepository.List();
        return prescriptions;
    }
}