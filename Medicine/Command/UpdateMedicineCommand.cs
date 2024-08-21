using CareTrack.Server.Models;
using CareTrack.Server.Repositories;
using MediatR;

namespace CareTrack.Server.Medicine.Command;

public class UpdateMedicineCommand : IRequest<Result<IMedicine>>
{
    public required IMedicine Medicine { get; init; }
}

public class UpdateMedicineCommandHandler(IMedicineRepository medicineRepository)
    : IRequestHandler<UpdateMedicineCommand, Result<IMedicine>>
{
    public async Task<Result<IMedicine>> Handle(UpdateMedicineCommand request, CancellationToken cancellationToken)
    {
        var medicine = await medicineRepository.Update(request.Medicine);
        return medicine;
    }
}