namespace dsw_pr02.Controllers;

using Microsoft.AspNetCore.Mvc;
using dsw_pr02.Models;
using dsw_pr02.Services;

/// <summary>
/// Gestiona los eventos: consulta, alta, modificación y eliminación.
/// </summary>
[ApiController]
[Route("api/eventos")]
[Produces("application/json")]
public class EventosController : ControllerBase
{
    private readonly IEventoService _eventoService;

    public EventosController(IEventoService eventoService)
    {
        _eventoService = eventoService;
    }

    /// <summary>
    /// Obtiene la lista de todos los eventos.
    /// </summary>
    /// <returns>Lista de eventos (puede estar vacía).</returns>
    /// <response code="200">Lista de eventos.</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<Evento>), StatusCodes.Status200OK)]
    public ActionResult<List<Evento>> ObtenerTodos()
    {
        return Ok(_eventoService.ObtenerTodos());
    }

    /// <summary>
    /// Obtiene un evento por su identificador.
    /// </summary>
    /// <param name="id">Identificador del evento.</param>
    /// <response code="200">Evento encontrado.</response>
    /// <response code="404">No existe un evento con ese id.</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Evento), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Evento> ObtenerPorId(int id)
    {
        var evento = _eventoService.ObtenerPorId(id);
        if (evento == null)
        {
            return NotFound(new { mensaje = $"No existe un evento con id {id}." });
        }

        return Ok(evento);
    }

    /// <summary>
    /// Crea un nuevo evento. El evento se registra siempre como activo.
    /// </summary>
    /// <remarks>
    /// Ejemplo de petición:
    ///
    ///     POST /api/eventos
    ///     {
    ///        "nombre": "Congreso de Software",
    ///        "categoria": "Tecnología",
    ///        "lugar": "Auditorio Principal",
    ///        "numeroMaximo": 200,
    ///        "activo": true
    ///     }
    ///
    /// Reglas: nombre, categoría y lugar son obligatorios; el aforo debe ser mayor que cero;
    /// el nombre no puede repetirse entre eventos.
    /// </remarks>
    /// <param name="evento">Datos del evento a crear.</param>
    /// <response code="201">Evento creado.</response>
    /// <response code="400">Datos inválidos (campo obligatorio vacío o aforo no válido).</response>
    /// <response code="409">Ya existe un evento con ese nombre.</response>
    [HttpPost]
    [ProducesResponseType(typeof(Evento), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public ActionResult<Evento> Agregar([FromBody] Evento evento)
    {
        try
        {
            _eventoService.Agregar(evento);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = evento.Id }, evento);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { mensaje = ex.Message });
        }
    }

    /// <summary>
    /// Actualiza un evento existente.
    /// </summary>
    /// <param name="id">Identificador del evento a modificar (debe coincidir con el id del cuerpo).</param>
    /// <param name="evento">Nuevos datos del evento.</param>
    /// <response code="204">Evento actualizado.</response>
    /// <response code="400">El id de la ruta no coincide con el del cuerpo, o datos inválidos.</response>
    /// <response code="404">No existe un evento con ese id.</response>
    /// <response code="409">Ya existe otro evento con ese nombre.</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult Actualizar(int id, [FromBody] Evento evento)
    {
        if (id != evento.Id)
        {
            return BadRequest(new { mensaje = "El id de la ruta no coincide con el id del evento." });
        }

        try
        {
            _eventoService.Actualizar(evento);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { mensaje = ex.Message });
        }
    }

    /// <summary>
    /// Elimina un evento. Solo se puede eliminar si está inactivo.
    /// </summary>
    /// <param name="id">Identificador del evento a eliminar.</param>
    /// <response code="204">Evento eliminado.</response>
    /// <response code="404">No existe un evento con ese id.</response>
    /// <response code="409">El evento está activo; desactívelo antes de eliminarlo.</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult Eliminar(int id)
    {
        try
        {
            _eventoService.Eliminar(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { mensaje = ex.Message });
        }
    }
}
