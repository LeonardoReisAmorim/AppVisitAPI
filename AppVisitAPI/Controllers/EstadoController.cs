using AppVisitAPI.DTOs.EstadoDTO;
using AppVisitAPI.Interfaces.IEstado;
using Microsoft.AspNetCore.Mvc;

namespace AppVisitAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EstadoController : ControllerBase
    {
        private readonly IEstadoService _IEstadoService;
        public EstadoController(IEstadoService iestadoService)
        {
            _IEstadoService = iestadoService;
        }

        [HttpGet]
        public async Task<ActionResult> GetEstados()
        {
            var estados = await _IEstadoService.GetEstado();
            return Ok(estados);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetEstadoById(int id)
        {
            var estado = await _IEstadoService.GetEstado(id);

            if (estado is null || !estado.Any())
            {
                return NotFound();
            }

            return Ok(estado);
        }

        [HttpPost]
        public async Task<ActionResult> CreateEstado([FromBody] CriarEstadoDTO estadoDTO)
        {
            var estadoCriado = await _IEstadoService.CreateEstado(estadoDTO);

            return CreatedAtAction(nameof(GetEstadoById), new { estadoCriado.Id }, estadoCriado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditEstado(int id, [FromBody] EditarEstadoDTO editarEstadoDTO)
        {
            var result = await _IEstadoService.UpdateEstado(id, editarEstadoDTO);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEstado(int id)
        {
            var result = await _IEstadoService.DeleteEstado(id);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
