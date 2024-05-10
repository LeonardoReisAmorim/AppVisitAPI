using AppVisitAPI.DTOs.CidadeDTO;
using AppVisitAPI.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppVisitAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        private readonly ICidadeService _iCidadeService;
        public CidadeController(ICidadeService iCidadeService)
        {
            _iCidadeService = iCidadeService;
        }

        [HttpGet]
        public async Task<ActionResult> GetCidades()
        {
            var cidades = await _iCidadeService.GetCidade();
            return Ok(cidades);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCidadeById(int id)
        {
            var cidade = await _iCidadeService.GetCidade(id);

            if (cidade is null || !cidade.Any())
            {
                return NotFound();
            }

            return Ok(cidade);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCidade([FromBody] CriarCidadeDTO cidadeDTO)
        {
            var cidadeCriada = await _iCidadeService.CreateCidade(cidadeDTO);

            return CreatedAtAction(nameof(GetCidadeById), new { cidadeCriada.Id }, cidadeCriada);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditCidade(int id, [FromBody] EditarCidadeDTO editarCidadeDTO)
        {
            var result = await _iCidadeService.UpdateCidade(id, editarCidadeDTO);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCidade(int id)
        {
            var result = await _iCidadeService.DeleteCidade(id);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
