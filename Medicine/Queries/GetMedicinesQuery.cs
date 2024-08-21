using CareTrack.Server.Models;
using CareTrack.Server.Repositories;
using MediatR;

namespace CareTrack.Server.Medicine.Queries;

public class GetMedicinesQuery : IRequest<Result<List<MedicineResult>>>
{
}

public class GetMedicinesQueryHandler
    : IRequestHandler<GetMedicinesQuery, Result<List<MedicineResult>>>
{
    private readonly IMedicineRepository medicineRepository;
    public GetMedicinesQueryHandler(IMedicineRepository medicineRepository)
    {
        this.medicineRepository = medicineRepository;
    }
    public async Task<Result<List<MedicineResult>>> Handle(GetMedicinesQuery request, CancellationToken cancellationToken)
    {
        var medicines = await medicineRepository.GetList();
        return medicines;
    }
}