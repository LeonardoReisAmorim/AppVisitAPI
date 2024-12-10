using Application.DTOs.CityDTO;
using Application.DTOs.TypePlaceDTO;
using Application.Interfaces;
using AppVisitAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppVisitAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class TypePlaceController : ControllerBase
    {
        private readonly ITypePlaceService _typePlaceService;
        public TypePlaceController(ITypePlaceService typePlaceService)
        {
            _typePlaceService = typePlaceService;
        }

        [HttpGet]
        public async Task<ActionResult> GetTypePlaces()
        {
            var typeplace = await _typePlaceService.GetTypePlace();
            return Ok(typeplace);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTypePlaceById(int id)
        {
            var typeplace = await _typePlaceService.GetTypePlace(id);

            if (typeplace is null || !typeplace.Any())
            {
                return NotFound();
            }

            return Ok(typeplace);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTypePlace([FromBody] TypePlaceDTO typeplaceDTO)
        {
            if (!User.IsAdmin())
            {
                BadRequest(new { error = "Apenas administradores podem adicionar registros" });
            }

            var typePlaceCreated = await _typePlaceService.CreateTypePlace(typeplaceDTO);

            return CreatedAtAction(nameof(GetTypePlaceById), new { typePlaceCreated.Id }, typePlaceCreated);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditTypePlace(int id, [FromBody] TypePlaceDTO updatetypePlaceDTO)
        {
            if (!User.IsAdmin())
            {
                BadRequest(new { error = "Apenas administradores podem atualizar registros" });
            }

            var result = await _typePlaceService.UpdateTypePlace(id, updatetypePlaceDTO);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCidade(int id)
        {
            if (!User.IsAdmin())
            {
                BadRequest(new { error = "Apenas administradores podem remover" });
            }

            var result = await _typePlaceService.DeleteTypePlace(id);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
