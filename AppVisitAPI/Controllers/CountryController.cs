using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AppVisitAPI.Extensions;
using Application.Interfaces;
using Application.DTOs.CountryDTO;

namespace AppVisitAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CountryController : ControllerBase
    {
        public ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<ActionResult> GetPaises()
        {
            var paises = await _countryService.GetPais();
            return Ok(paises);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPaisById(int id)
        {
            var pais = await _countryService.GetPais(id);

            if(pais is null || !pais.Any())
            {
                return NotFound();
            }

            return Ok(pais);    
        }

        [HttpPost]
        public async Task<ActionResult> CreatePais([FromBody] CountryDTO paisDTO)
        {
            if (!User.IsAdmin())
            {
                BadRequest(new { error = "Apenas administradores podem adicionar registros" });
            }

            var paisCriado = await _countryService.CreatePais(paisDTO);

            return CreatedAtAction(nameof(GetPaisById), new { paisCriado.Id }, paisCriado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditPais(int id, [FromBody] CountryDTO editarPaisDTO)
        {
            if (!User.IsAdmin())
            {
                BadRequest(new { error = "Apenas administradores podem atualizar registros" });
            }

            var result = await _countryService.UpdatePais(id, editarPaisDTO);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePais(int id)
        {
            if (!User.IsAdmin())
            {
                BadRequest(new { error = "Apenas administradores podem remover" });
            }

            var result = await _countryService.DeletePais(id);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
