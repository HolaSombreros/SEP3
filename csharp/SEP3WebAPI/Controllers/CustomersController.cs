using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEP3Library.Models;
using SEP3Library.UIModels;
using SEP3WebAPI.Data;


namespace SEP3WebAPI.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase {
        private ICustomerService service;

        public CustomersController(ICustomerService service) {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Customer>> GetCustomerAsync([FromQuery] string email,
            [FromQuery] string password) {
            try {
                if (email == null || password == null) {
                    return BadRequest("Input both email and password!");
                }

                Customer customer = await service.GetCustomerAsync(email, password);
                if (customer == null)
                    return NotFound("Email not registered");
                if (customer.Password.Equals(password))
                    return Ok(customer);
                return NotFound("Wrong password");
            }
            catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IList<Customer>>> GetCustomersByIndexAsync([FromQuery] int index) {
            try {
                IList<Customer> customers = await service.GetCustomersByIndexAsync(index);
                return Ok(customers);
            } catch (NullReferenceException e) {
                return NotFound(e.Message);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> AddCustomerAsync([FromBody] CustomerModel customer) {
            try {
                Customer cust = await service.GetCustomerAsync(customer.Email, customer.Password);
                if (cust != null)
                    return Conflict("The email and password are already used");
                cust = await service.AddCustomerAsync(customer);
                return Created($"{cust.Email}", cust);
            }
            catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("{customerId:int}")]
        public async Task<ActionResult<Customer>> GetCustomerAsync([FromRoute] int customerId) {
            try {
                Customer customer = await service.GetCustomerAsync(customerId);
                return Ok(customer);
            }
            catch (NullReferenceException e) {
                return NotFound(e.Message);
            }
            catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpPut]
        [Route("{customerId:int}")]
        public async Task<ActionResult<Customer>> UpdateCustomerAsync([FromRoute] int customerId,
            [FromBody] UpdateCustomerModel customer) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            try {
                Customer updated = await service.UpdateCustomerAsync(customerId, customer);
                return Ok(updated);
            }
            catch (NullReferenceException e) {
                return NotFound(e.Message);
            }
            catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route( "{customerId:int}/notifications")]
        public async Task<ActionResult<IList<Notification>>> GetNotificationsAsync([FromRoute] int customerId, [FromQuery] int index) {
            try {
                IList<Notification> notifications = await service.GetNotificationsAsync(customerId, index);
                return Ok(notifications);
            } catch (NullReferenceException e) {
                return NotFound(e.Message);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        [Route("{customerId:int}/notifications/{notificationId:int}")]
        public async Task<ActionResult<Notification>> UpdateSeenNotificationAsync([FromBody] Notification notification, [FromRoute] int customerId, [FromRoute] int notificationId) {
            try {
                Notification notification1 = await service.UpdateSeenNotificationAsync(customerId, notificationId);
                Console.WriteLine(notification1.Text);
                return Ok(notification1);
            } catch (NullReferenceException e) {
                return NotFound(e.Message);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }
    }
}