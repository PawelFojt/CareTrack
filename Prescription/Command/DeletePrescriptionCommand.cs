using CareTrack.Server.Models;
using CareTrack.Server.Repositories;
using MediatR;

namespace CareTrack.Server.Prescription.Command;

public class DeletePrescriptionCommand : IRequest<Result<IPrescription>>
{
    public  int Id { get; init; }
}

public class DeletePrescriptionCommandHandler : 
    IRequestHandler<DeletePrescriptionCommand, Result<IPrescription>>
{
    private readonly IPrescriptionRepository prescriptionRepository;
    public DeletePrescriptionCommandHandler(IPrescriptionRepository prescriptionRepository)
    {
        this.prescriptionRepository = prescriptionRepository;
    }
    public async Task<Result<IPrescription>> Handle(DeletePrescriptionCommand request, CancellationToken cancellationToken)
    {
        var prescription = await prescriptionRepository.Delete(request.Id);
        return prescription;
    }
}