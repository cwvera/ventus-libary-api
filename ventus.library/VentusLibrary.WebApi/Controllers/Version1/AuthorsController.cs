using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VentusLibrary.Application.Authors.Commands;
using VentusLibrary.Application.Authors.Dtos;
using VentusLibrary.Application.Authors.Queries;
using VentusLibrary.Commons.Responses;

namespace VentusLibrary.WebApi.Controllers.Version1;

/// <summary>API de Autores.</summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Lista autores activos.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<AuthorDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListAsync()
    {
        IReadOnlyList<AuthorDto> result = await _mediator.Send(new ListActiveAuthorsQuery());
        return Ok(result);
    }

    /// <summary>
    /// Obtiene un autor por su identificador (solo activos).
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AuthorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        AuthorDto? result = await _mediator.Send(new GetAuthorByIdQuery(id));
        if (result is null) return NoContent();
        return Ok(result);
    }

    /// <summary>
    /// Crea un nuevo autor o reactiva uno soft-deleted si coincide el email.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(CommandResponse<AuthorDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateAuthorCommand command)
    {
        CommandResponse<AuthorDto> result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Actualiza un autor existente.
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(CommandResponse<AuthorDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateAuthorCommand command)
    {
        if (id != command.Id) return BadRequest("El identificador de ruta no coincide.");
        CommandResponse<AuthorDto> result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Borrado lógico de un autor.
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> SoftDeleteAsync(int id)
    {
        bool ok = await _mediator.Send(new SoftDeleteAuthorCommand(id));
        if (!ok) return NotFound();
        return NoContent();
    }
}
