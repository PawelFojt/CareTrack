using CareTrack.Server.Modules.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CareTrack.Server.Modules.Presentation.Controllers;

public class CommonController : Controller
{

    protected IActionResult ConvertResult<T>(Result<T> result)
    {
        return StatusCode((int)result.StatusCode, result);
    }

    protected IActionResult WrongInputArgument()
    {
        return BadRequest("Niepoprawne dane wejściowe");
    }
}