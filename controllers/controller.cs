namespace dsw_pr02.Controllers;

using Microsoft.AspNetCore.Mvc;
using dsw_pr02.Models;
using dsw_pr02.Services;

[ApiController]
[Route("api/eventos")]
public class EventosController : ControllerBase
{
    private readonly IEventoService _eventoService;

    public EventosController(IEventoService eventoService)
    {
        _eventoService = eventoService;
    }

    // GET api/eventos
    [HttpGet]
    public ActionResult<List<Evento>> GetAll()
    {
        return Ok(_eventoService.GetAll());
    }

    // GET api/eventos/5
    [HttpGet("{id:int}")]
    public ActionResult<Evento> GetById(int id)
    {
        var evento = _eventoService.GetById(id);
        if (evento == null)
        {
            return NotFound(new { mensaje = $"No existe un evento con id {id}." });
        }

        return Ok(evento);
    }

    // POST api/eventos
    [HttpPost]
    public ActionResult<Evento> Add([FromBody] Evento evento)
    {
        try
        {
            _eventoService.Add(evento);
            return CreatedAtAction(nameof(GetById), new { id = evento.Id }, evento);
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

    // PUT api/eventos/5
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] Evento evento)
    {
        if (id != evento.Id)
        {
            return BadRequest(new { mensaje = "El id de la ruta no coincide con el id del evento." });
        }

        try
        {
            _eventoService.Update(evento);
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

    // DELETE api/eventos/5
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        try
        {
            _eventoService.Delete(id);
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
