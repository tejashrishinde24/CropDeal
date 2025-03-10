using CropDealBackend.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CropDealBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddOnController : ControllerBase
    {
        private readonly IAddon addOnRepository;
        public AddOnController(IAddon _addOnRepository) {
            addOnRepository = _addOnRepository;
        }
        // GET: api/<AddOnController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AddOnController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AddOnController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AddOnController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AddOnController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
