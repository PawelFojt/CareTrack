using CareTrack.Domain.Models;
using CareTrack.Domain.Repositories;
using MediatR;

namespace CareTrack.Application.Medicine.Queries;

public class GetMedicinesQuery : IRequest<Result<List<IMedicine>>>
{
}

public class GetMedicinesQueryHandler(IMedicineRepository medicineRepository)
    : IRequestHandler<GetMedicinesQuery, Result<List<IMedicine>>>
{
    public async Task<Result<List<IMedicine>>> Handle(GetMedicinesQuery request, CancellationToken cancellationToken)
    {
        var medicines = await medicineRepository.GetList();
        return medicines;
    }
}