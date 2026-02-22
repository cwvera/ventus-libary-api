using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VentusLibrary.Application.Books.Commands;
using VentusLibrary.Application.Books.Dtos;
using VentusLibrary.Application.Books.Queries;
using VentusLibrary.Commons.Responses;

namespace VentusLibrary.WebApi.Controllers.Version1;

/// <summary>API de Libros.</summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IMediator _mediator;

    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Lista libros activos incluyendo nombre de autor y nombre de género.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<BookListItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListAsync()
    {
        IReadOnlyList<BookListItemDto> result = await _mediator.Send(new ListActiveBooksWithDetailsQuery());
        return Ok(result);
    }

    /// <summary>Obtiene un libro por Id (solo activos).</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        BookDto? result = await _mediator.Send(new GetBookByIdQuery(id));
        if (result is null) return NoContent();
        return Ok(result);
    }

    /// <summary>Crea un nuevo libro aplicando límites activos.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(CommandResponse<BookDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateBookCommand command)
    {
        CommandResponse<BookDto> result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>Actualiza un libro existente.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(CommandResponse<BookDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateBookCommand command)
    {
        if (id != command.Id) return BadRequest("El identificador de ruta no coincide.");
        CommandResponse<BookDto> result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>Borrado lógico de un libro.</summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> SoftDeleteAsync(int id)
    {
        bool ok = await _mediator.Send(new SoftDeleteBookCommand(id));
        if (!ok) return NotFound();
        return NoContent();
    }
}
