using AppVisitAPI.DTOs.CidadeDTO;
using AppVisitAPI.DTOs.PaisDTO;
using AppVisitAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppVisitAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        private CidadeService _cidadeService;
        public CidadeController(CidadeService cidade)
        {
            _cidadeService = cidade;
        }

        [HttpGet]
        public ActionResult GetCidades()
        {
            var cidades = _cidadeService.GetCidade();
            return Ok(cidades);
        }

        [HttpGet("{id}")]
        public ActionResult GetCidadeById(int id)
        {
            var cidade = _cidadeService.GetCidade(id);
            if (cidade == null)
            {
                return NotFound();
            }

            return Ok(cidade);
        }

        [HttpPost]
        public ActionResult CreateCidade(CriarCidadeDTO cidadeDTO)
        {
            var cidadeCriada = _cidadeService.CreateCidade(cidadeDTO);

            return CreatedAtAction(nameof(GetCidadeById), new { cidadeCriada.Id }, cidadeCriada);
        }

        [HttpPut("{id}")]
        public ActionResult EditCidade(int id, [FromBody] EditarCidadeDTO editarCidadeDTO)
        {
            var result = _cidadeService.UpdateCidade(id, editarCidadeDTO);
            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCidade(int id)
        {
            var result = _cidadeService.DeleteCidade(id);
            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
