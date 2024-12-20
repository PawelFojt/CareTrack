using CareTrack.Server.Modules.Domain.Models;
using CareTrack.Server.Modules.Domain.Repositories;
using MediatR;

namespace CareTrack.Server.Modules.Application.Medicine.Command;

public class DeleteMedicineCommand : IRequest<Result<IMedicine>>
{
    public int Id { get; init; }
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