using CareTrack.Domain.Models;
using CareTrack.Domain.Repositories;
using MediatR;

namespace CareTrack.Application.Prescription.Command;

public class UpdatePrescriptionCommand : IRequest<Result<IPrescription>>
{
    public required Domain.Models.Prescription Prescription { get; set; }
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
