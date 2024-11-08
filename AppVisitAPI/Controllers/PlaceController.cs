using Application.DTOs.PlaceDTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppVisitAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class PlaceController : ControllerBase
    {
        private readonly IPlaceService _placeService;
        
        public PlaceController(IPlaceService placeService)
        {
            _placeService = placeService;
        }

        [HttpGet]
        public async Task<ActionResult> GetLugares()
        {
            var lugares = await _placeService.GetLugar();
            return Ok(lugares);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetLugarById(int id)
        {
            var lugar = await _placeService.GetLugar(id);

            if (lugar is null || !lugar.Any())
            {
                return NotFound();
            }

            return Ok(lugar);
        }

        [HttpGet("utilizationPlaceVR/{id}")]
        public async Task<ActionResult> GetUtilizacaoLugarVRById(int id)
        {
            var utilizacaoLugarVR = await _placeService.GetUtilizacaoLugarVRById(id);

            if (String.IsNullOrEmpty(utilizacaoLugarVR))
            {
                return NotFound();
            }

            return Ok(utilizacaoLugarVR);
        }

        [HttpPost]
        public async Task<ActionResult> CreateLugar([FromBody] PlaceDTO lugarDTO)
        {
            if(lugarDTO.FileVRId == 0 || lugarDTO.CityId == 0)
            {
                return BadRequest(new { error = "necessário informar o arquivo e a cidade" });
            }

            var lugarCriado = await _placeService.CreateLugar(lugarDTO);

            return CreatedAtAction(nameof(GetLugarById), new { lugarCriado.Id }, lugarCriado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditLugar(int id, [FromBody] PlaceDTO editarLugarDTO)
        {
            if(editarLugarDTO.CityId == 0 || editarLugarDTO.FileVRId == 0)
            {
                return BadRequest(new { error = "necessário informar o arquivo e a cidade" });
            }

            var result = await _placeService.UpdateLugar(id, editarLugarDTO);
            
            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLugar(int id)
        {
            var result = await _placeService.DeleteLugar(id);
            
            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
