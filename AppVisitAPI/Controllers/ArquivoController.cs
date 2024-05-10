using AppVisitAPI.DTOs.ArquivoDTO;
using AppVisitAPI.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AppVisitAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArquivoController : ControllerBase
    {
        private readonly IArquivoService _iArquivoService;
        public ArquivoController(IArquivoService iArquivoService)
        {
            _iArquivoService = iArquivoService;
        }

        [HttpGet("{id}")]
        public IActionResult GetArquivosById(int id)
        {
            var arquivo = _iArquivoService.GetArquivoById(id);

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
        public async Task<IActionResult> GetDadosArquivos()
        {
            var dadosarquivo = await _iArquivoService.GetDadosArquivo();
            return Ok(dadosarquivo);
        }

        [HttpGet("dadosArquivos/{id}")]
        public async Task<IActionResult> GetDadosArquivosById(int id)
        {
            var dadosarquivo = await _iArquivoService.GetDadosArquivo(id);

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
                return BadRequest(new { error = "Somente arquivos .zip são importados. Por favor tente novamente" });
            }

            var inserirArquivoDTO = JsonConvert.DeserializeObject<InserirArquivoDTO>(Request.Form.FirstOrDefault().Value);

            if (file.Length > 0)
            {
                using (var Stream = new MemoryStream())
                {
                    file.CopyTo(Stream);

                    lerArquivo = _iArquivoService.CreateArquivo(Stream.ToArray(), inserirArquivoDTO);
                }
            }

            return CreatedAtAction(nameof(GetArquivosById), new { lerArquivo.Id }, lerArquivo);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateArquivo(int id)
        {
            var file = Request.Form.Files[0];

            if (!file.FileName.Contains(".zip"))
            {
                return BadRequest(new { error = "Somente arquivos .zip são importados. Por favor tente novamente" });
            }

            var EditarArquivoDTO = JsonConvert.DeserializeObject<EditarArquivo>(Request.Form.FirstOrDefault().Value);

            if (file.Length > 0)
            {
                using (var Stream = new MemoryStream())
                {
                    file.CopyTo(Stream);
                    EditarArquivoDTO.Arquivo = Stream.ToArray();
                }
            }

            var result = _iArquivoService.UpdateArquivo(id, EditarArquivoDTO);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteArquivo(int id)
        {
            var result = _iArquivoService.DeleteArquivo(id);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
