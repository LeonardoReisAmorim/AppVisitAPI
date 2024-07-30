using AppVisitAPI.DTOs.ArquivoDTO;
using AppVisitAPI.Interfaces.IArquivo;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AppVisitAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArquivoController : ControllerBase
    {
        private readonly IArquivoService _IArquivoService;
        public ArquivoController(IArquivoService iarquivoService)
        {
            _IArquivoService = iarquivoService;
        }

        [HttpGet("{id}")]
        public IActionResult GetArquivosById(int id, [FromQuery] int chunkNumber, [FromQuery] int chunkSize)
        {
            var result = _IArquivoService.GetArquivoById(id);

            if (result is null || !result.Any())
            {
                return NotFound();
            }

            int offset = chunkNumber * chunkSize;
            if (offset >= result.Length)
            {
                return NotFound();
            }

            int length = Math.Min(chunkSize, result.Length - offset);
            byte[] chunk = new byte[length];
            Array.Copy(result, offset, chunk, 0, length);

            return File(chunk, "application/zip", "arquivo.zip");
        }

        [HttpGet("dadosArquivos")]
        public async Task<IActionResult> GetDadosArquivos()
        {
            var dadosarquivo = await _IArquivoService.GetDadosArquivo();
            return Ok(dadosarquivo);
        }

        [HttpGet("dadosArquivos/{id}")]
        public async Task<IActionResult> GetDadosArquivosById(int id)
        {
            var dadosarquivo = await _IArquivoService.GetDadosArquivo(id);

            if (dadosarquivo is null || !dadosarquivo.Any())
            {
                return NotFound();
            }

            return Ok(dadosarquivo);
        }

        [HttpPost]
        public async Task<IActionResult> CreateArquivo()
        {
            var file = Request.Form.Files[0];
            var lerArquivo = new LerArquivoDTO();
            
            if (!file.FileName.Contains(".zip") && !file.FileName.Contains(".rar"))
            {
                return BadRequest(new { error = "Somente arquivos compactados são importados. Por favor tente novamente" });
            }

            var inserirArquivoDTO = JsonConvert.DeserializeObject<InserirArquivoDTO>(Request.Form.FirstOrDefault().Value);

            if (file.Length > 0)
            {
                using (var Stream = new MemoryStream())
                {
                    file.CopyTo(Stream);

                    lerArquivo = await _IArquivoService.CreateArquivoAsync(Stream.ToArray(), inserirArquivoDTO);
                }
            }

            return CreatedAtAction(nameof(GetArquivosById), new { lerArquivo.Id }, lerArquivo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArquivo(int id)
        {
            var file = Request.Form.Files[0];

            if (!file.FileName.Contains(".zip") && !file.FileName.Contains(".rar"))
            {
                return BadRequest(new { error = "Somente arquivos compactados são importados. Por favor tente novamente" });
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

            var result = await _IArquivoService.UpdateArquivo(id, EditarArquivoDTO);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArquivo(int id)
        {
            var result = await _IArquivoService.DeleteArquivo(id);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
