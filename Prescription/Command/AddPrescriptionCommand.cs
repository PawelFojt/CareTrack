using CareTrack.Server.Models;
using CareTrack.Server.Repositories;
using MediatR;

namespace CareTrack.Server.Prescription.Command;

public class AddPrescriptionCommand : IRequest<Result<IPrescription>>
{
    public required Models.Prescription Prescription { get; set; }
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