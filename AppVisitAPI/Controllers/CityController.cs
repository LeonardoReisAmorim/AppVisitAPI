using Application.DTOs.CityDTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppVisitAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<ActionResult> GetCidades()
        {
            var cidades = await _cityService.GetCidade();
            return Ok(cidades);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCidadeById(int id)
        {
            var cidade = await _cityService.GetCidade(id);

            if (cidade is null || !cidade.Any())
            {
                return NotFound();
            }

            return Ok(cidade);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCidade([FromBody] CityDTO cidadeDTO)
        {
            var cidadeCriada = await _cityService.CreateCidade(cidadeDTO);

            return CreatedAtAction(nameof(GetCidadeById), new { cidadeCriada.Id }, cidadeCriada);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditCidade(int id, [FromBody] CityDTO editarCidadeDTO)
        {
            var result = await _cityService.UpdateCidade(id, editarCidadeDTO);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCidade(int id)
        {
            var result = await _cityService.DeleteCidade(id);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
