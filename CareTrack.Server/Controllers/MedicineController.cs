using CareTrack.Domain.Models;
using CareTrack.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Medicine = CareTrack.Infrastructure.Entities.Medicine;

namespace CareTrack.Server.Controllers;

[ApiController]
[Route("Medicine")]
public class MedicineController(IMediator mediator, IMedicineRepository medicineRepository) : CommonController(mediator)
{
    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        return ConvertResult(await medicineRepository.GetList());
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody]Medicine? medicine)
    {
        if (medicine is null) return WrongInputArgument();
        return ConvertResult(await medicineRepository.Add(medicine));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody]Medicine? medicine)
    {
        if (medicine is null) return WrongInputArgument();
        return ConvertResult(await medicineRepository.Update(medicine));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        if (id == 0) return WrongInputArgument();
        return ConvertResult(await medicineRepository.Delete(id));
    }
}