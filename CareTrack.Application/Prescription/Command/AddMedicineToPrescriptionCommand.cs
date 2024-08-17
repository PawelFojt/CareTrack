using CareTrack.Domain.Models;
using CareTrack.Domain.Repositories;
using MediatR;

namespace CareTrack.Application.Prescription.Command;

public class AddMedicineToPrescriptionCommand : IRequest<Result<IPrescription>>
{
    public required int MedicineId { get; init; }
    public required int PrescriptionId { get; init; }
}

public class AddMedicineToPrescriptionCommandHandler(IPrescriptionRepository prescriptionRepository)
    : IRequestHandler<AddMedicineToPrescriptionCommand, Result<IPrescription>>
{
    public async Task<Result<IPrescription>> Handle(AddMedicineToPrescriptionCommand request, CancellationToken cancellationToken)
    {
        var prescription = await prescriptionRepository.AddMedicineToPrescription(request.PrescriptionId, request.MedicineId);
        return prescription;
    }
}