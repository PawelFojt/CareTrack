using CareTrack.Server.Modules.Domain.Models;
using CareTrack.Server.Modules.Domain.Repositories;
using MediatR;

namespace CareTrack.Server.Modules.Application.Medicine.Command;

public class UpdateMedicineCommand : IRequest<Result<IMedicine>>
{
    public required MedicineResult Medicine { get; init; }
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