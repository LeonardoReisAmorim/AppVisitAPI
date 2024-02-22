using AppVisitAPI.DTOs.LugarDTO;
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
        public async Task<ActionResult> GetLugares()
        {
            var lugares = await _lugarService.GetLugar();
            return Ok(lugares);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetLugarById(int id)
        {
            var lugar = await _lugarService.GetLugar(id);

            if (lugar.Count <= 0)
            {
                return NotFound();
            }

            return Ok(lugar);
        }

        [HttpPost]
        public async Task<ActionResult> CreateLugar([FromBody] InserirLugarDTO lugarDTO)
        {
            if(lugarDTO.ArquivoId == 0)
            {
                return BadRequest("Não é possível criar um lugar sem um arquivo");
            }

            var lugarCriado = await _lugarService.CreateLugar(lugarDTO);

            return CreatedAtAction(nameof(GetLugarById), new { lugarCriado.Id }, lugarCriado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditLugar(int id, [FromBody] EditarLugarDTO editarLugarDTO)
        {
            var result = await _lugarService.UpdateLugar(id, editarLugarDTO);
            
            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLugar(int id)
        {
            var result = await _lugarService.DeleteLugar(id);
            
            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
