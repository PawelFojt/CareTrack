using CareTrack.Domain.Models;
using CareTrack.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Medicine = CareTrack.Infrastructure.Entities.Medicine;

namespace CareTrack.Server.Controllers;

[ApiController]
[Route("Medicine")]
public class MedicineController : Controller
{
    private readonly IMedicineRepository _medicineRepository;

    public MedicineController(IMedicineRepository medicineRepository)
    {
        _medicineRepository = medicineRepository;
    }
    [HttpGet]
    public async Task<ActionResult<List<IMedicine>>> GetList()
    {
        return Ok(await _medicineRepository.GetList());
    }

    [HttpPost("Add")]
    public async Task<ActionResult<IMedicine>> Add([FromBody]Medicine medicine)
    {
        return Ok(await _medicineRepository.Add(medicine));
    }

    [HttpPut]
    public async Task<ActionResult<IMedicine>> Update([FromBody]Medicine medicine)
    {
        return Ok(await _medicineRepository.Update(medicine));
    }

    [HttpDelete]
    public async Task<ActionResult<IMedicine>> Delete(int id)
    {
        return Ok(await _medicineRepository.Delete(id));
    }
}