using AppVisitAPI.DTOs.LugarDTO;
using AppVisitAPI.DTOs.PaisDTO;
using AppVisitAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppVisitAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LugarController : ControllerBase
    {
        private LugarService _lugarService;
        
        public LugarController(LugarService lugarService)
        {
            _lugarService = lugarService;
        }

        [HttpGet]
        public ActionResult GetLugares()
        {
            var lugares = _lugarService.GetLugar();
            return Ok(lugares);
        }

        [HttpGet("{id}")]
        public ActionResult GetLugarById(int id)
        {
            var lugar = _lugarService.GetLugar(id);

            if (lugar.Count <= 0)
            {
                return NotFound();
            }

            return Ok(lugar);
        }

        [HttpPost]
        public ActionResult CreateLugar(InserirLugarDTO lugarDTO)
        {
            var lugarCriado = _lugarService.CreateLugar(lugarDTO);

            return CreatedAtAction(nameof(GetLugarById), new { lugarCriado.Id }, lugarCriado);
        }

        [HttpPut("{id}")]
        public ActionResult EditLugar(int id, [FromBody] EditarLugarDTO editarLugarDTO)
        {
            var result = _lugarService.UpdateLugar(id, editarLugarDTO);
            
            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteLugar(int id)
        {
            var result = _lugarService.DeleteLugar(id);
            
            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
