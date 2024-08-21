using CareTrack.Server.Models;
using CareTrack.Server.Repositories;
using MediatR;

namespace CareTrack.Server.Medicine.Command;

public class DeleteMedicineCommand : IRequest<Result<IMedicine>>
{
    public int Id { get; init; }
}

public class DeleteMedicineCommandHandler
    : IRequestHandler<DeleteMedicineCommand, Result<IMedicine>>
{
    private readonly IMedicineRepository medicineRepository;
    public DeleteMedicineCommandHandler(IMedicineRepository medicineRepository)
    {
        this.medicineRepository = medicineRepository;
    }
    public async Task<Result<IMedicine>> Handle(DeleteMedicineCommand request, CancellationToken cancellationToken)
    {
        var medicine = await medicineRepository.Delete(request.Id);
        return medicine;
    }
}