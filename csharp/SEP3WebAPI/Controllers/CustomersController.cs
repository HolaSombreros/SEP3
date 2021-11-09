using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEP3Library.Model;
using SEP3WebAPI.Data;

namespace SEP3WebAPI.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase {
        private IRestService service;
        
        public CustomersController(IRestService service) {
            this.service = service;
        }

        [HttpGet]
        [Route("{email}/{password}")]
        public async Task<ActionResult<Customer>> GetCustomerAsync([FromRoute] string email, [FromRoute] string password) {
            try {
                Customer customer = await service.GetCustomerAsync(email, password);
                return Ok(customer);
            }
            catch (NullReferenceException e) {
                return NotFound(e.Message);
            }
            catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Customer>>
            AddCustomerAsync([FromBody] Customer customer) {
            try {
                Customer cust = await service.AddCustomerAsync(customer);
                return Created($"{cust.Email}", cust);
            }
            catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }
    }
}