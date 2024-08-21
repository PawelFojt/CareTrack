using CareTrack.Server.Models;
using CareTrack.Server.Repositories;
using MediatR;

namespace CareTrack.Server.Prescription.Queries;

public class GetPrescriptionsWithMedicinesQuery : IRequest<Result<IEnumerable<IPrescriptionWithMedicines>>>
{
}

public class GetPrescriptionsWithMedicinesQueryHandler
    : IRequestHandler<GetPrescriptionsWithMedicinesQuery, Result<IEnumerable<IPrescriptionWithMedicines>>>
{
    private readonly IPrescriptionRepository prescriptionRepository;
    public GetPrescriptionsWithMedicinesQueryHandler(IPrescriptionRepository prescriptionRepository)
    {
        this.prescriptionRepository = prescriptionRepository;
    }
    public async Task<Result<IEnumerable<IPrescriptionWithMedicines>>> Handle(
        GetPrescriptionsWithMedicinesQuery request,
        CancellationToken cancellationToken)
    {
        var prescriptions = await prescriptionRepository.List();
        return prescriptions;
    }
}