using CropDealBackend.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CropDealBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankDetailController : ControllerBase
    {
        private readonly IBankDetail bankDetailRepository;
        public BankDetailController(IBankDetail _bankDetailRepository)
        {
            bankDetailRepository = _bankDetailRepository;
        }
        // GET: api/<BankDetailController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<BankDetailController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BankDetailController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BankDetailController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BankDetailController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
