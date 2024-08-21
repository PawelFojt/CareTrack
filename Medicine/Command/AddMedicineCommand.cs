using CareTrack.Server.Models;
using CareTrack.Server.Repositories;
using MediatR;

namespace CareTrack.Server.Medicine.Command;

public class AddMedicineCommand : IRequest<Result<IMedicine>>
{
    public IMedicine Medicine { get; init; }
}

public class AddMedicineCommandHandler
    : IRequestHandler<AddMedicineCommand, Result<IMedicine>>
{
    private readonly IMedicineRepository medicineRepository;
    public AddMedicineCommandHandler(IMedicineRepository medicineRepository)
    {
        this.medicineRepository = medicineRepository;
    }
    public async Task<Result<IMedicine>> Handle(AddMedicineCommand request, CancellationToken cancellationToken)
    {
        var medicine = await medicineRepository.Add(request.Medicine);
        return medicine;
    }
}