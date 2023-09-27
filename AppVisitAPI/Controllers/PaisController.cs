using AppVisitAPI.DTOs.PaisDTO;
using AppVisitAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppVisitAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaisController : ControllerBase
    {
        public PaisService _paisService;

        public PaisController(PaisService paisService)
        {
            _paisService = paisService;
        }

        [HttpGet]
        public ActionResult GetPaises()
        {
            var paises = _paisService.GetPais();
            return Ok(paises);
        }

        [HttpGet("{id}")]
        public ActionResult GetPaisById(int id)
        {
            var pais = _paisService.GetPais(id);
            if(pais == null)
            {
                return NotFound();
            }

            return Ok(pais);    
        }

        [HttpPost]
        public ActionResult CreatePais(CriarPaisDTO paisDTO)
        {
            var paisCriado = _paisService.CreatePais(paisDTO);

            return CreatedAtAction(nameof(GetPaisById), new { paisCriado.Id }, paisCriado);
        }

        [HttpPut("{id}")]
        public ActionResult EditPais(int id, [FromBody] EditarPaisDTO editarPaisDTO)
        {
            var result = _paisService.UpdatePais(id, editarPaisDTO);
            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePais(int id)
        {
            var result = _paisService.DeletePais(id);
            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
