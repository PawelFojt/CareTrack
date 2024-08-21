using CareTrack.Server.Models;
using CareTrack.Server.Repositories;
using MediatR;

namespace CareTrack.Server.Prescription.Queries;

public class GetPrescriptionWithMedicinesQuery : IRequest<Result<IPrescriptionWithMedicines>>
{
    public int Id { get; set; }
}

public class GetPrescriptionWithMedicinesQueryHandler
    : IRequestHandler<GetPrescriptionWithMedicinesQuery, Result<IPrescriptionWithMedicines>>
{
    private readonly IPrescriptionRepository prescriptionRepository;
    public GetPrescriptionWithMedicinesQueryHandler(IPrescriptionRepository prescriptionRepository)
    {
        this.prescriptionRepository = prescriptionRepository;
    }
    public async Task<Result<IPrescriptionWithMedicines>> Handle(
        GetPrescriptionWithMedicinesQuery request,
        CancellationToken cancellationToken)
    {
        var prescription = await prescriptionRepository.Get(request.Id);
        return prescription;
    }
}