﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEP3Library.Model;
using SEP3Library.UIModels;
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
        public async Task<ActionResult<Customer>> GetCustomerAsync([FromQuery] string email, [FromQuery] string password) {
            try {
                Console.WriteLine(email);
                if (email == null || password == null) {
                    return BadRequest("Input both email and password!");
                }
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
        public async Task<ActionResult<Customer>> AddCustomerAsync([FromBody] CustomerModel customer) {
            try {
                Customer cust = await service.AddCustomerAsync(customer);
                return Created($"{cust.Email}", cust);
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
            } catch (NullReferenceException e) {
                return NotFound(e.Message);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("{customerId:int}/wishlist/{itemId:int}")]
        public async Task<ActionResult> RemoveWishlistedItem([FromRoute] int customerId, [FromRoute] int itemId) {
            try {
                await service.RemoveWishlistedItem(customerId, itemId);
                return Ok();
            } catch (NullReferenceException e) {
                return NotFound(e.Message);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }
    }
}