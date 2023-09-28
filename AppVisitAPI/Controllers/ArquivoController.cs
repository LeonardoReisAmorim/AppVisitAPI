using AppVisitAPI.DTOs.ArquivoDTO;
using AppVisitAPI.DTOs.EstadoDTO;
using AppVisitAPI.Models;
using AppVisitAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppVisitAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArquivoController : ControllerBase
    {
        private ArquivoService _arquivoService;
        public ArquivoController(ArquivoService arquivoService)
        {
            _arquivoService = arquivoService;
        }

        [HttpGet("{id}")]
        public IActionResult GetArquivosById(int id)
        {
            var arquivo = _arquivoService.GetArquivoById(id);

            if (arquivo == null)
            {
                return NotFound();
            }

            return Ok(arquivo);
        }

        [HttpPost]
        public ActionResult CreateArquivo()
        {
            var file = Request.Form.Files[0];

            var arquivoCriado = new LerArquivoDTO();

            if (file.Length > 0)
            {
                using (var Stream = new MemoryStream())
                {
                    file.CopyTo(Stream);

                    arquivoCriado = _arquivoService.CreateArquivo(Stream.ToArray());
                }
            }

            return CreatedAtAction(nameof(GetArquivosById), new { arquivoCriado.Id }, arquivoCriado);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateArquivo(int id, [FromBody] EditarArquivo arquivoDTO)
        {
            var result = _arquivoService.UpdateArquivo(id, arquivoDTO);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteArquivo(int id)
        {
            var result = _arquivoService.DeleteArquivo(id);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
