using CareTrack.Server.Modules.Domain.Models;
using CareTrack.Server.Modules.Domain.Repositories;
using MediatR;

namespace CareTrack.Server.Modules.Application.Prescription.Command;

public class UpdatePrescriptionCommand : IRequest<Result<IPrescription>>
{
    public PrescriptionResult PrescriptionResult { get; set; }
}

public class UpdatePrescriptionCommandHandler
    : IRequestHandler<UpdatePrescriptionCommand, Result<IPrescription>>
{
    private readonly IPrescriptionRepository prescriptionRepository;
    public UpdatePrescriptionCommandHandler(IPrescriptionRepository prescriptionRepository)
    {
        this.prescriptionRepository = prescriptionRepository;
    }
    public async Task<Result<IPrescription>> Handle(UpdatePrescriptionCommand request, CancellationToken cancellationToken)
    {
        var prescription = await prescriptionRepository.Update(request.PrescriptionResult);
        return prescription;
    }
}
