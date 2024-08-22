using CareTrack.Server.Modules.Domain.Models;
using CareTrack.Server.Modules.Domain.Repositories;
using MediatR;

namespace CareTrack.Server.Modules.Application.Medicine.Command;

public class UpdateMedicineCommand : IRequest<Result<IMedicine>>
{
    public IMedicine Medicine { get; init; }
}

public class UpdateMedicineCommandHandler
    : IRequestHandler<UpdateMedicineCommand, Result<IMedicine>>
{
    private readonly IMedicineRepository medicineRepository;
    public UpdateMedicineCommandHandler(IMedicineRepository medicineRepository)
    {
        this.medicineRepository = medicineRepository;
    }
    public async Task<Result<IMedicine>> Handle(UpdateMedicineCommand request, CancellationToken cancellationToken)
    {
        var medicine = await medicineRepository.Update(request.Medicine);
        return medicine;
    }
}