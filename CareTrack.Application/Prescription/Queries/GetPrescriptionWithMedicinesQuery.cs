using CareTrack.Domain.Models;
using CareTrack.Domain.Repositories;
using MediatR;

namespace CareTrack.Application.Prescription.Queries;

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