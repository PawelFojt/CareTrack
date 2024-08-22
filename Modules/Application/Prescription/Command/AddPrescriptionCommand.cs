using CareTrack.Server.Modules.Domain.Models;
using CareTrack.Server.Modules.Domain.Repositories;
using MediatR;

namespace CareTrack.Server.Modules.Application.Prescription.Command;

public class AddPrescriptionCommand : IRequest<Result<IPrescription>>
{
    public PrescriptionResult PrescriptionResult { get; set; }
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