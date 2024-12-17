using CareTrack.Server.Modules.Domain.Models;
using CareTrack.Server.Modules.Domain.Repositories;
using MediatR;

namespace CareTrack.Server.Modules.Application.Prescription.Command;

public class DeletePrescriptionCommand : IRequest<Result<IPrescription>>
{
    public  int Id { get; init; }
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