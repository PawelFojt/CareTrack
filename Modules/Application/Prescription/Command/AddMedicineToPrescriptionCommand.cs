using CareTrack.Server.Modules.Domain.Models;
using CareTrack.Server.Modules.Domain.Repositories;
using MediatR;

namespace CareTrack.Server.Modules.Application.Prescription.Command;

public class AddMedicineToPrescriptionCommand : IRequest<Result<IPrescription>>
{
    public int MedicineId { get; init; }
    public int PrescriptionId { get; init; }
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