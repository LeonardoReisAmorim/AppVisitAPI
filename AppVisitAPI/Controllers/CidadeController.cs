using AppVisitAPI.DTOs.CidadeDTO;
using AppVisitAPI.Interfaces.ICidade;
using Microsoft.AspNetCore.Mvc;

namespace AppVisitAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        private readonly ICidadeService _ICidadeService;
        public CidadeController(ICidadeService cidadeService)
        {
            _ICidadeService = cidadeService;
        }

        [HttpGet]
        public async Task<ActionResult> GetCidades()
        {
            var cidades = await _ICidadeService.GetCidade();
            return Ok(cidades);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCidadeById(int id)
        {
            var cidade = await _ICidadeService.GetCidade(id);

            if (cidade is null || !cidade.Any())
            {
                return NotFound();
            }

            return Ok(cidade);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCidade([FromBody] CriarCidadeDTO cidadeDTO)
        {
            var cidadeCriada = await _ICidadeService.CreateCidade(cidadeDTO);

            return CreatedAtAction(nameof(GetCidadeById), new { cidadeCriada.Id }, cidadeCriada);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditCidade(int id, [FromBody] EditarCidadeDTO editarCidadeDTO)
        {
            var result = await _ICidadeService.UpdateCidade(id, editarCidadeDTO);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCidade(int id)
        {
            var result = await _ICidadeService.DeleteCidade(id);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
