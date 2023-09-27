using AppVisitAPI.DTOs.EstadoDTO;
using AppVisitAPI.DTOs.PaisDTO;
using AppVisitAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppVisitAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EstadoController : ControllerBase
    {
        private EstadoService _estadoService;
        public EstadoController(EstadoService estadoService)
        {
            _estadoService = estadoService;
        }

        [HttpGet]
        public ActionResult GetEstados()
        {
            var estados = _estadoService.GetEstado();
            return Ok(estados);
        }

        [HttpGet("{id}")]
        public ActionResult GetEstadoById(int id)
        {
            var estado = _estadoService.GetEstado(id);
            if (estado == null)
            {
                return NotFound();
            }

            return Ok(estado);
        }

        [HttpPost]
        public ActionResult CreateEstado(CriarEstadoDTO estadoDTO)
        {
            var estadoCriado = _estadoService.CreateEstado(estadoDTO);

            return CreatedAtAction(nameof(GetEstadoById), new { estadoCriado.Id }, estadoCriado);
        }

        [HttpPut("{id}")]
        public ActionResult EditEstado(int id, [FromBody] EditarEstadoDTO editarEstadoDTO)
        {
            var result = _estadoService.UpdateEstado(id, editarEstadoDTO);
            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteEstado(int id)
        {
            var result = _estadoService.DeleteEstado(id);
            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
