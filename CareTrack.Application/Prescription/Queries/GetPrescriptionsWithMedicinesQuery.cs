using CareTrack.Domain.Models;
using CareTrack.Domain.Repositories;
using MediatR;

namespace CareTrack.Application.Prescription.Queries;

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