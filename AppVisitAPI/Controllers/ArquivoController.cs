using Microsoft.AspNetCore.Mvc;

namespace AppVisitAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArquivoController : ControllerBase
    {
        // GET: api/<ArquivoController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ArquivoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ArquivoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ArquivoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ArquivoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
