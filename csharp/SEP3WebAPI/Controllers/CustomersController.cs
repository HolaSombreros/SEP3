using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEP3Library.Models;
using SEP3Library.UIModels;
using SEP3WebAPI.Data;
using SEP3WebAPI.Mediator.Messages;

namespace SEP3WebAPI.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase {
        private IRestService service;

        public CustomersController(IRestService service) {
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
        
        [HttpGet]
        [Route("{customerId:int}/order")]
        public async Task<ActionResult<IList<Order>>> GetAllOrdersByCustomer([FromRoute] int customerId, [FromQuery] int index) {
            try {
                IList<Order> orders = await service.GetOrdersByCustomerAsync(customerId, index);
                return Ok(orders);
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
        [Route("{customerId:int}/wishlist")]
        public async Task<ActionResult<IList<Item>>> GetCustomerWishlistAsync([FromRoute] int customerId) {
            try {
                IList<Item> wishlist = await service.GetCustomerWishlistAsync(customerId);
                return Ok(wishlist);
            }
            catch (NullReferenceException e) {
                return NotFound(e.Message);
            }
            catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        [Route("{customerId:int}/wishlist/{itemId:int}")]
        public async Task<ActionResult<Item>> AddToWishlist([FromRoute] int customerId, [FromRoute] int itemId) {
            try {
                Item item = await service.AddToWishlistAsync(customerId, itemId);
                return Ok(item);
            }
            catch (NullReferenceException e) {
                return NotFound(e.Message);
            }
            catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("{customerId:int}/wishlist/{itemId:int}")]
        public async Task<ActionResult> RemoveWishlistedItemAsync([FromRoute] int customerId, [FromRoute] int itemId) {
            try {
                await service.RemoveWishlistedItemAsync(customerId, itemId);
                return Ok();
            }
            catch (NullReferenceException e) {
                return NotFound(e.Message);
            }
            catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        [Route("{customerId:int}/shoppingbasket")]
        public async Task<ActionResult<Item>> AddShoppingCartAsync([FromBody] Item item, [FromRoute] int customerId) {
            Console.WriteLine("customercontroller");
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            try {
                Item item1 = await service.AddToShoppingCartAsync(item, customerId);
                return Ok(item1);
            }
            catch (NullReferenceException e) {
                return NotFound(e.Message);
            }
            catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("{customerId:int}/shoppingbasket")]
        public async Task<ActionResult> GetShoppingCartAsync([FromRoute] int customerId) {
            try {
                IList<Item> shoppingCart = await service.GetShoppingCartAsync(customerId);
                return Ok(shoppingCart);
            }
            catch (NullReferenceException e) {
                return NotFound(e.Message);
            }
            catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        [Route("{customerId:int}/shoppingbasket/{itemId:int}")]
        public async Task<ActionResult<Item>> EditShoppingCartAsync([FromBody] Item item, [FromRoute] int customerId,
            [FromRoute] int itemId) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            try {
                Item item1 = await service.UpdateShoppingCartAsync(item, itemId, customerId);
                return Ok(item);
            }
            catch (NullReferenceException e) {
                return NotFound(e.Message);
            }
            catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("{customerId:int}/shoppingbasket/{itemId:int}")]
        public async Task<ActionResult>
            RemoveFromShoppingCartAsync([FromRoute] int itemId, [FromRoute] int customerId) {
            try {
                await service.RemoveFromShoppingCartAsync(itemId, customerId);
                return Ok();
            }
            catch (NullReferenceException e) {
                return NotFound(e.Message);
            }
            catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

       
    }
}