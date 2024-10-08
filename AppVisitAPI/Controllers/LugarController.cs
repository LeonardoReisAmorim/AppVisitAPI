﻿using AppVisitAPI.DTOs.LugarDTO;
using AppVisitAPI.Interfaces.ILugar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppVisitAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class LugarController : ControllerBase
    {
        private readonly ILugarService _ILugarService;
        
        public LugarController(ILugarService iLugarService)
        {
            _ILugarService = iLugarService;
        }

        [HttpGet]
        public async Task<ActionResult> GetLugares()
        {
            var lugares = await _ILugarService.GetLugar();
            return Ok(lugares);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetLugarById(int id)
        {
            var lugar = await _ILugarService.GetLugar(id);

            if (lugar is null || !lugar.Any())
            {
                return NotFound();
            }

            return Ok(lugar);
        }

        [HttpGet("utilizationPlaceVR/{id}")]
        public async Task<ActionResult> GetUtilizacaoLugarVRById(int id)
        {
            var utilizacaoLugarVR = await _ILugarService.GetUtilizacaoLugarVRById(id);

            if (String.IsNullOrEmpty(utilizacaoLugarVR))
            {
                return NotFound();
            }

            return Ok(utilizacaoLugarVR);
        }

        [HttpPost]
        public async Task<ActionResult> CreateLugar([FromBody] InserirLugarDTO lugarDTO)
        {
            if(lugarDTO.ArquivoId == 0 || lugarDTO.CidadeId == 0)
            {
                return BadRequest(new { error = "necessário informar o arquivo e a cidade" });
            }

            var lugarCriado = await _ILugarService.CreateLugar(lugarDTO);

            return CreatedAtAction(nameof(GetLugarById), new { lugarCriado.Id }, lugarCriado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditLugar(int id, [FromBody] EditarLugarDTO editarLugarDTO)
        {
            if(editarLugarDTO.CidadeId == 0 || editarLugarDTO.ArquivoId == 0)
            {
                return BadRequest(new { error = "necessário informar o arquivo e a cidade" });
            }

            var result = await _ILugarService.UpdateLugar(id, editarLugarDTO);
            
            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLugar(int id)
        {
            var result = await _ILugarService.DeleteLugar(id);
            
            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
