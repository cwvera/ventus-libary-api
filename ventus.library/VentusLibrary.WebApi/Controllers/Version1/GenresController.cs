using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VentusLibrary.Application.Genres.Dtos;
using VentusLibrary.Application.Genres.Queries;

namespace VentusLibrary.WebApi.Controllers.Version1;

/// <summary>API de Géneros.</summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class GenresController : ControllerBase
{
    private readonly IMediator _mediator;

    public GenresController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>Lista géneros activos.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListAsync()
    {
        IReadOnlyList<GenreDto> result = await _mediator.Send(new ListActiveGenresQuery());
        return Ok(result);
    }

    /// <summary>Obtiene un género por Id (solo activos).</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        GenreDto? result = await _mediator.Send(new GetGenreByIdQuery(id));
        if (result is null) return NoContent();
        return Ok(result);
    }
}
