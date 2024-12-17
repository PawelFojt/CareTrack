using CareTrack.Server.Modules.Domain.Models;
using CareTrack.Server.Modules.Domain.Repositories;
using MediatR;

namespace CareTrack.Server.Modules.Application.Prescription.Command;

public class UpdatePrescriptionCommand : IRequest<Result<IPrescription>>
{
    public required PrescriptionResult Prescription { get; set; }
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
