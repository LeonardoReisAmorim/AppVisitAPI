using AppVisitAPI.DTOs.PaisDTO;
using AppVisitAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppVisitAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaisController : ControllerBase
    {
        public PaisService _paisService;

        public PaisController(PaisService paisService)
        {
            _paisService = paisService;
        }

        [HttpGet]
        public async Task<ActionResult> GetPaises()
        {
            var paises = await _paisService.GetPais();
            return Ok(paises);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPaisById(int id)
        {
            var pais = await _paisService.GetPais(id);

            if(pais is null || !pais.Any())
            {
                return NotFound();
            }

            return Ok(pais);    
        }

        [HttpPost]
        public async Task<ActionResult> CreatePais([FromBody] CriarPaisDTO paisDTO)
        {
            var paisCriado = await _paisService.CreatePais(paisDTO);

            return CreatedAtAction(nameof(GetPaisById), new { paisCriado.Id }, paisCriado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditPais(int id, [FromBody] EditarPaisDTO editarPaisDTO)
        {
            var result = await _paisService.UpdatePais(id, editarPaisDTO);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePais(int id)
        {
            var result = await _paisService.DeletePais(id);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
