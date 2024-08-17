using CareTrack.Domain.Models;
using CareTrack.Domain.Repositories;
using MediatR;

namespace CareTrack.Application.Prescription.Command;

public class AddPrescriptionCommand : IRequest<Result<IPrescription>>
{
    public required Domain.Models.Prescription Prescription { get; set; }
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