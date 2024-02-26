using AppVisitAPI.DTOs.ArquivoDTO;
using AppVisitAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AppVisitAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArquivoController : ControllerBase
    {
        private readonly ArquivoService _arquivoService;
        public ArquivoController(ArquivoService arquivoService)
        {
            _arquivoService = arquivoService;
        }

        [HttpGet("{id}")]
        public IActionResult GetArquivosById(int id)
        {
            var arquivo = _arquivoService.GetArquivoById(id);

            if (arquivo is null || !arquivo.Any())
            {
                return NotFound();
            }

            var result = new FileContentResult(arquivo, "application/zip")
            {
                FileDownloadName = "arquivo.zip"
            };

            return result;
        }

        [HttpGet("dadosArquivos")]
        public IActionResult GetDadosArquivos()
        {
            var dadosarquivo = _arquivoService.GetDadosArquivo();
            return Ok(dadosarquivo);
        }

        [HttpGet("dadosArquivos/{id}")]
        public IActionResult GetDadosArquivosById(int id)
        {
            var dadosarquivo = _arquivoService.GetDadosArquivo(id);

            if (dadosarquivo is null || !dadosarquivo.Any())
            {
                return NotFound();
            }

            return Ok(dadosarquivo);
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

            var inserirArquivoDTO = JsonConvert.DeserializeObject<InserirArquivoDTO>(Request.Form.FirstOrDefault().Value);

            if (file.Length > 0)
            {
                using (var Stream = new MemoryStream())
                {
                    file.CopyTo(Stream);

                    lerArquivo = _arquivoService.CreateArquivo(Stream.ToArray(), inserirArquivoDTO);
                }
            }

            return CreatedAtAction(nameof(GetArquivosById), new { lerArquivo.Id }, lerArquivo);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateArquivo(int id)
        {
            var file = Request.Form.Files[0];
            var EditarArquivoDTO = JsonConvert.DeserializeObject<EditarArquivo>(Request.Form.FirstOrDefault().Value);

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
