using CareTrack.Domain.Models;
using CareTrack.Domain.Repositories;
using MediatR;

namespace CareTrack.Application.Prescription.Command;

public class DeletePrescriptionCommand : IRequest<Result<IPrescription>>
{
    public required int Id { get; init; }
}

public class DeletePrescriptionCommandHandler(IPrescriptionRepository prescriptionRepository) : 
    IRequestHandler<DeletePrescriptionCommand, Result<IPrescription>>
{
    public async Task<Result<IPrescription>> Handle(DeletePrescriptionCommand request, CancellationToken cancellationToken)
    {
        var prescription = await prescriptionRepository.Delete(request.Id);
        return prescription;
    }
}