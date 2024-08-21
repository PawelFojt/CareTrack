using CareTrack.Server.Models;
using CareTrack.Server.Repositories;
using MediatR;

namespace CareTrack.Server.Prescription.Command;

public class AddPrescriptionCommand : IRequest<Result<IPrescription>>
{
    public Models.PrescriptionResult PrescriptionResult { get; set; }
}

public class AddPrescriptionCommandHandler
    : IRequestHandler<AddPrescriptionCommand, Result<IPrescription>>
{
    private readonly IPrescriptionRepository prescriptionRepository;
    public AddPrescriptionCommandHandler(IPrescriptionRepository prescriptionRepository)
    {
        this.prescriptionRepository = prescriptionRepository;
    }
    public async Task<Result<IPrescription>> Handle(AddPrescriptionCommand request, CancellationToken cancellationToken)
    {
        var prescription = await prescriptionRepository.Add(request.PrescriptionResult);
        return prescription;
    }
}