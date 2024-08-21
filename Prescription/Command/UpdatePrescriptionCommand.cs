using CareTrack.Server.Models;
using CareTrack.Server.Repositories;
using MediatR;

namespace CareTrack.Server.Prescription.Command;

public class UpdatePrescriptionCommand : IRequest<Result<IPrescription>>
{
    public required Models.Prescription Prescription { get; set; }
}

public class UpdatePrescriptionCommandHandler(IPrescriptionRepository prescriptionRepository)
    : IRequestHandler<UpdatePrescriptionCommand, Result<IPrescription>>
{
    public async Task<Result<IPrescription>> Handle(UpdatePrescriptionCommand request, CancellationToken cancellationToken)
    {
        var prescription = await prescriptionRepository.Update(request.Prescription);
        return prescription;
    }
}
