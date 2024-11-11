using Application.DTOs.FileVRDTO;
using Application.Interfaces;
using AppVisitAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AppVisitAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class FileVRController : ControllerBase
    {
        private readonly IFileVRService _fileVRService;
        public FileVRController(IFileVRService fileVRService)
        {
            _fileVRService = fileVRService;
        }

        [HttpGet("{id}")]
        public IActionResult GetArquivosById(int id)
        {
            var fileContent = _fileVRService.GetArquivoById(id);

            return File(fileContent, "application/zip", "arquivo.zip");
        }

        [HttpGet("dadosArquivos")]
        public async Task<IActionResult> GetDadosArquivos()
        {
            var dadosarquivo = await _fileVRService.GetDadosArquivo();
            return Ok(dadosarquivo);
        }

        [HttpGet("dadosArquivos/{id}")]
        public async Task<IActionResult> GetDadosArquivosById(int id)
        {
            var dadosarquivo = await _fileVRService.GetDadosArquivo(id);

            if (dadosarquivo is null || !dadosarquivo.Any())
            {
                return NotFound();
            }

            return Ok(dadosarquivo);
        }

        [HttpPost]
        public async Task<IActionResult> CreateArquivo()
        {
            if (!User.IsAdmin())
            {
                BadRequest(new { error = "Apenas administradores podem adicionar registros" });
            }

            var file = Request.Form.Files[0];
            var lerArquivo = new ReadFileVRDTO();
            
            if (!file.FileName.Contains(".zip"))
            {
                return BadRequest(new { error = "Somente arquivos .zip são importados. Por favor tente novamente" });
            }

            var inserirArquivoDTO = JsonConvert.DeserializeObject<AddFileVRDTO>(Request.Form.FirstOrDefault().Value);

            if (file.Length > 0)
            {
                using (var Stream = new MemoryStream())
                {
                    file.CopyTo(Stream);

                    lerArquivo = await _fileVRService.CreateArquivoAsync(Stream.ToArray(), inserirArquivoDTO);
                }
            }

            return CreatedAtAction(nameof(GetArquivosById), new { lerArquivo.Id }, lerArquivo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArquivo(int id)
        {
            if (!User.IsAdmin())
            {
                BadRequest(new { error = "Apenas administradores podem atualizar registros" });
            }

            var file = Request.Form.Files[0];

            if (!file.FileName.Contains(".zip"))
            {
                return BadRequest(new { error = "Somente arquivos .zip são importados. Por favor tente novamente" });
            }

            var EditarArquivoDTO = JsonConvert.DeserializeObject<UpdateFileVRDTO>(Request.Form.FirstOrDefault().Value);

            if (file.Length > 0)
            {
                using (var Stream = new MemoryStream())
                {
                    file.CopyTo(Stream);
                    EditarArquivoDTO.Content = Stream.ToArray();
                }
            }

            var result = await _fileVRService.UpdateArquivo(id, EditarArquivoDTO);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArquivo(int id)
        {
            if (!User.IsAdmin())
            {
                BadRequest(new { error = "Apenas administradores podem remover" });
            }

            var result = await _fileVRService.DeleteArquivo(id);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
