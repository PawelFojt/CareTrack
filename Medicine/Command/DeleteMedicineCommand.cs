using CareTrack.Server.Models;
using CareTrack.Server.Repositories;
using MediatR;

namespace CareTrack.Server.Medicine.Command;

public class DeleteMedicineCommand : IRequest<Result<IMedicine>>
{
    public required int Id { get; init; }
}

public class DeleteMedicineCommandHandler(IMedicineRepository medicineRepository)
    : IRequestHandler<DeleteMedicineCommand, Result<IMedicine>>
{
    public async Task<Result<IMedicine>> Handle(DeleteMedicineCommand request, CancellationToken cancellationToken)
    {
        var medicine = await medicineRepository.Delete(request.Id);
        return medicine;
    }
}