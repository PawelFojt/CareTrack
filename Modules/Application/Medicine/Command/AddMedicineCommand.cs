using CareTrack.Server.Modules.Domain.Models;
using CareTrack.Server.Modules.Domain.Repositories;
using MediatR;

namespace CareTrack.Server.Modules.Application.Medicine.Command;

public class AddMedicineCommand : IRequest<Result<IMedicine>>
{
    public required MedicineResult Medicine { get; init; }
}

public class AddMedicineCommandHandler(IMedicineRepository medicineRepository)
    : IRequestHandler<AddMedicineCommand, Result<IMedicine>>
{
    public async Task<Result<IMedicine>> Handle(AddMedicineCommand request, CancellationToken cancellationToken)
    {
        var medicine = await medicineRepository.Add(request.Medicine);
        return medicine;
    }
}