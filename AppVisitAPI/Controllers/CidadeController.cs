using AppVisitAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppVisitAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        private CidadeService _cidadeService;
        public CidadeController(CidadeService cidade)
        {
            _cidadeService = cidade;
        }

        // GET: api/<CidadeController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        // GET api/<CidadeController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok();
        }

        // POST api/<CidadeController>
        [HttpPost]
        public IActionResult Post([FromBody] string a)
        {
            return Ok();
        }

        // PUT api/<CidadeController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return Ok();
        }

        // DELETE api/<CidadeController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
