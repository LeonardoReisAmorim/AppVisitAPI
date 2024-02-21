using AppVisitAPI.DTOs.ArquivoDTO;
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

            var result = new FileContentResult(arquivo, "application/zip")
            {
                FileDownloadName = "arquivo.zip"
            };

            return result;
        }

        [HttpPost]
        public IActionResult CreateArquivo()
        {
            var file = Request.Form.Files[0];
            var lerArquivo = new LerArquivoDTO();

            if (!file.FileName.Contains(".zip"))
            {
                return BadRequest("Somente arquivos .zip são importados");
            }

            if (file.Length > 0)
            {
                using (var Stream = new MemoryStream())
                {
                    file.CopyTo(Stream);

                    lerArquivo = _arquivoService.CreateArquivo(Stream.ToArray());
                }
            }

            return CreatedAtAction(nameof(GetArquivosById), new { lerArquivo.Id }, lerArquivo);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateArquivo(int id)
        {
            var file = Request.Form.Files[0];
            var EditarArquivoDTO = new EditarArquivo();

            if (file.Length > 0)
            {
                using (var Stream = new MemoryStream())
                {
                    file.CopyTo(Stream);

                    EditarArquivoDTO.Arquivo = Stream.ToArray();
                }
            }

            
            var result = _arquivoService.UpdateArquivo(id, EditarArquivoDTO);

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
