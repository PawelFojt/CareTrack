using CareTrack.Server.Modules.Domain.Models;
using CareTrack.Server.Modules.Domain.Repositories;
using MediatR;

namespace CareTrack.Server.Modules.Application.Prescription.Command;

public class AddPrescriptionCommand : IRequest<Result<IPrescription>>
{
    public required PrescriptionResult Prescription { get; set; }
}

public class AddPrescriptionCommandHandler(IPrescriptionRepository prescriptionRepository)
    : IRequestHandler<AddPrescriptionCommand, Result<IPrescription>>
{
    public async Task<Result<IPrescription>> Handle(AddPrescriptionCommand request, CancellationToken cancellationToken)
    {
        var prescription = await prescriptionRepository.Add(request.Prescription);
        return prescription;
    }
}