﻿using AppVisitAPI.DTOs.ArquivoDTO;
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
        public async Task<IActionResult> GetArquivosById(int id)
        {
            var arquivo = await _arquivoService.GetArquivoById(id);

            if (arquivo == null)
            {
                return NotFound();
            }

            return Ok(arquivo);
        }

        [HttpPost]
        public async Task<ActionResult> CreateArquivo()
        {
            var file = Request.Form.Files[0];

            var arquivoCriado = new LerArquivoDTO();

            if (file.Length > 0)
            {
                using (var Stream = new MemoryStream())
                {
                    file.CopyTo(Stream);

                    arquivoCriado = await _arquivoService.CreateArquivo(Stream.ToArray());
                }
            }

            return CreatedAtAction(nameof(GetArquivosById), new { arquivoCriado.Id }, arquivoCriado);
        }
        //TODO - 415 - unsupported media type save apk file in sql server - RESOLVER
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArquivo(int id)
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

            
            var result = await _arquivoService.UpdateArquivo(id, EditarArquivoDTO);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArquivo(int id)
        {
            var result = await _arquivoService.DeleteArquivo(id);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
