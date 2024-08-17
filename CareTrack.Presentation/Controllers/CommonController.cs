using CareTrack.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CareTrack.Presentation.Controllers;

public class CommonController : Controller
{
    private readonly IMediator _mediator;

    public CommonController(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected IActionResult ConvertResult<T>(Result<T> result)
    {
        return StatusCode((int)result.StatusCode, result);
    }

    protected IActionResult WrongInputArgument()
    {
        return BadRequest("Niepoprawne dane wejściowe");
    }
}