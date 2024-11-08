using Application.DTOs.StateDTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppVisitAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class StateController : ControllerBase
    {
        private readonly IStateService _stateService;
        public StateController(IStateService stateService)
        {
            _stateService = stateService;
        }

        [HttpGet]
        public async Task<ActionResult> GetEstados()
        {
            var estados = await _stateService.GetEstado();
            return Ok(estados);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetEstadoById(int id)
        {
            var estado = await _stateService.GetEstado(id);

            if (estado is null || !estado.Any())
            {
                return NotFound();
            }

            return Ok(estado);
        }

        [HttpPost]
        public async Task<ActionResult> CreateEstado([FromBody] StateDTO estadoDTO)
        {
            var estadoCriado = await _stateService.CreateEstado(estadoDTO);

            return CreatedAtAction(nameof(GetEstadoById), new { estadoCriado.Id }, estadoCriado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditEstado(int id, [FromBody] StateDTO editarEstadoDTO)
        {
            var result = await _stateService.UpdateEstado(id, editarEstadoDTO);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEstado(int id)
        {
            var result = await _stateService.DeleteEstado(id);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
