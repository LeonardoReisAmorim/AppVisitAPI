using AppVisitAPI.DTOs.LugarDTO;
using AppVisitAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppVisitAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LugarController : ControllerBase
    {
        private readonly LugarService _lugarService;
        
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

            if (lugar is null || !lugar.Any())
            {
                return NotFound();
            }

            return Ok(lugar);
        }

        [HttpGet("utilizationPlaceVR/{id}")]
        public async Task<ActionResult> GetUtilizacaoLugarVRById(int id)
        {
            var utilizacaoLugarVR = await _lugarService.GetUtilizacaoLugarVRById(id);

            if (String.IsNullOrEmpty(utilizacaoLugarVR))
            {
                return NotFound();
            }

            return Ok(utilizacaoLugarVR);
        }

        [HttpPost]
        public async Task<ActionResult> CreateLugar([FromBody] InserirLugarDTO lugarDTO)
        {
            if(lugarDTO.ArquivoId == 0 || lugarDTO.CidadeId == 0)
            {
                return BadRequest(new { error = "necessário informar o arquivo e a cidade" });
            }

            var lugarCriado = await _lugarService.CreateLugar(lugarDTO);

            return CreatedAtAction(nameof(GetLugarById), new { lugarCriado.Id }, lugarCriado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditLugar(int id, [FromBody] EditarLugarDTO editarLugarDTO)
        {
            if(editarLugarDTO.CidadeId == 0 || editarLugarDTO.ArquivoId == 0)
            {
                return BadRequest(new { error = "necessário informar o arquivo e a cidade" });
            }

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
