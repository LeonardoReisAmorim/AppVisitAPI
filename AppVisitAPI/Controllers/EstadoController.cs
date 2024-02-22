using AppVisitAPI.DTOs.EstadoDTO;
using AppVisitAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppVisitAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EstadoController : ControllerBase
    {
        private readonly EstadoService _estadoService;
        public EstadoController(EstadoService estadoService)
        {
            _estadoService = estadoService;
        }

        [HttpGet]
        public async Task<ActionResult> GetEstados()
        {
            var estados = await _estadoService.GetEstado();
            return Ok(estados);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetEstadoById(int id)
        {
            var estado = await _estadoService.GetEstado(id);

            if (estado.Count <= 0)
            {
                return NotFound();
            }

            return Ok(estado);
        }

        [HttpPost]
        public async Task<ActionResult> CreateEstado([FromBody] CriarEstadoDTO estadoDTO)
        {
            var estadoCriado = await _estadoService.CreateEstado(estadoDTO);

            return CreatedAtAction(nameof(GetEstadoById), new { estadoCriado.Id }, estadoCriado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditEstado(int id, [FromBody] EditarEstadoDTO editarEstadoDTO)
        {
            var result = await _estadoService.UpdateEstado(id, editarEstadoDTO);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEstado(int id)
        {
            var result = await _estadoService.DeleteEstado(id);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
