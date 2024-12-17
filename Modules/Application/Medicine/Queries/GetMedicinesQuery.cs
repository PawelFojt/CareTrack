using System.Net;
using CareTrack.Server.Modules.Domain.Models;
using CareTrack.Server.Modules.Domain.Repositories;
using MediatR;

namespace CareTrack.Server.Modules.Application.Medicine.Queries;

public class GetMedicinesQuery : IRequest<Result<IEnumerable<IMedicine>>>
{
}

public class GetMedicinesQueryHandler(IMedicineRepository medicineRepository)
    : IRequestHandler<GetMedicinesQuery, Result<IEnumerable<IMedicine>>>
{
    public async Task<Result<IEnumerable<IMedicine>>> Handle(GetMedicinesQuery request, CancellationToken cancellationToken)
    {
        var medicines = await medicineRepository.List();
        if (medicines.IsError || medicines.Value is null) 
            return Result<IEnumerable<IMedicine>>.Error("Error while fetching medicines", HttpStatusCode.InternalServerError);
        return new (medicines.Value);
    }
}