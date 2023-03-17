using Tufesa_Dev_Test.API.Interfaces;
using Tufesa_Dev_Test.Core.Interfaces;
using Tufesa_Dev_Test.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tufesa_Dev_Test.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase, ICustomerController
    {
        private readonly ICustomerRepository _CustomersRepository;
        public CustomersController(ICustomerRepository CustomerRepository) {
            _CustomersRepository = CustomerRepository;
        }

        // POST api/<CustomersController>
        [HttpPost]
        public async Task<ActionResult> Create(string values)
        {
            Customer Customer = new Customer();
            JsonConvert.PopulateObject(values, Customer);
            return await _CustomersRepository.Create(Customer); 
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Read(int id)
        {
            return await _CustomersRepository.Read(id.ToString());
        }

        // GET: api/<CustomersController>
        [HttpGet]
        public async Task<ActionResult> ReadAll()
        {
            return await _CustomersRepository.ReadAll();
        }
        // GET: api/<CustomersController>/ByName
        [HttpGet]
        [Route("ByName")]
        public async Task<ActionResult> ReadAllSortByName()
        {
            return await _CustomersRepository.ReadAllSortByName();
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] string values)
        {
            Customer Customer = JsonConvert.DeserializeObject<Customer>(values);
            return await _CustomersRepository.Update(id, Customer);
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await _CustomersRepository.Delete(id.ToString());
        }

    }
}
