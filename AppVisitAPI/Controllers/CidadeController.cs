using AppVisitAPI.DTOs.CidadeDTO;
using AppVisitAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppVisitAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        private readonly CidadeService _cidadeService;
        public CidadeController(CidadeService cidade)
        {
            _cidadeService = cidade;
        }

        [HttpGet]
        public async Task<ActionResult> GetCidades()
        {
            var cidades = await _cidadeService.GetCidade();
            return Ok(cidades);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCidadeById(int id)
        {
            var cidade = await _cidadeService.GetCidade(id);

            if (cidade.Count <= 0)
            {
                return NotFound();
            }

            return Ok(cidade);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCidade([FromBody] CriarCidadeDTO cidadeDTO)
        {
            var cidadeCriada = await _cidadeService.CreateCidade(cidadeDTO);

            return CreatedAtAction(nameof(GetCidadeById), new { cidadeCriada.Id }, cidadeCriada);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditCidade(int id, [FromBody] EditarCidadeDTO editarCidadeDTO)
        {
            var result = await _cidadeService.UpdateCidade(id, editarCidadeDTO);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCidade(int id)
        {
            var result = await _cidadeService.DeleteCidade(id);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
