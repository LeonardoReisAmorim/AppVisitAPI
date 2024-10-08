﻿using AppVisitAPI.DTOs.PaisDTO;
using AppVisitAPI.Interfaces.IPais;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AppVisitAPI.Extensions;

namespace AppVisitAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PaisController : ControllerBase
    {
        public IPaisService _IPaisService;

        public PaisController(IPaisService iPaisService)
        {
            _IPaisService = iPaisService;
        }

        [HttpGet]
        public async Task<ActionResult> GetPaises()
        {
            var paises = await _IPaisService.GetPais();
            return Ok(paises);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPaisById(int id)
        {
            var pais = await _IPaisService.GetPais(id);

            if(pais is null || !pais.Any())
            {
                return NotFound();
            }

            return Ok(pais);    
        }

        [HttpPost]
        public async Task<ActionResult> CreatePais([FromBody] CriarPaisDTO paisDTO)
        {
            var paisCriado = await _IPaisService.CreatePais(paisDTO);

            return CreatedAtAction(nameof(GetPaisById), new { paisCriado.Id }, paisCriado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditPais(int id, [FromBody] EditarPaisDTO editarPaisDTO)
        {
            var result = await _IPaisService.UpdatePais(id, editarPaisDTO);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePais(int id)
        {
            if (!User.IsAdmin())
            {
                BadRequest("apenas administradores podem remover");
            }

            var result = await _IPaisService.DeletePais(id);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
