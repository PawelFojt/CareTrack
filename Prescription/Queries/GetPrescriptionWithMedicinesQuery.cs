using CareTrack.Server.Models;
using CareTrack.Server.Repositories;
using MediatR;

namespace CareTrack.Server.Prescription.Queries;

public class GetPrescriptionWithMedicinesQuery : IRequest<Result<IPrescriptionWithMedicines>>
{
    public required int Id { get; set; }
}

public class GetPrescriptionWithMedicinesQueryHandler(IPrescriptionRepository prescriptionRepository)
    : IRequestHandler<GetPrescriptionWithMedicinesQuery, Result<IPrescriptionWithMedicines>>
{
    public async Task<Result<IPrescriptionWithMedicines>> Handle(
        GetPrescriptionWithMedicinesQuery request,
        CancellationToken cancellationToken)
    {
        var prescription = await prescriptionRepository.Get(request.Id);
        return prescription;
    }
}