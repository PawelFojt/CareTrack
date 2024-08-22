using System.Net;
using CareTrack.Server.Models;
using CareTrack.Server.Repositories;
using MediatR;

namespace CareTrack.Server.Medicine.Queries;

public class GetMedicinesQuery : IRequest<Result<IEnumerable<IMedicine>>>
{
}

public class GetMedicinesQueryHandler
    : IRequestHandler<GetMedicinesQuery, Result<IEnumerable<IMedicine>>>
{
    private readonly IMedicineRepository medicineRepository;
    public GetMedicinesQueryHandler(IMedicineRepository medicineRepository)
    {
        this.medicineRepository = medicineRepository;
    }
    public async Task<Result<IEnumerable<IMedicine>>> Handle(GetMedicinesQuery request, CancellationToken cancellationToken)
    {
        var medicines = await medicineRepository.GetList();
        if (medicines.IsError || medicines.Value is null) 
            return Result<IEnumerable<IMedicine>>.Error("Error while fetching medicines", HttpStatusCode.InternalServerError);
        return new (medicines.Value);
    }
}