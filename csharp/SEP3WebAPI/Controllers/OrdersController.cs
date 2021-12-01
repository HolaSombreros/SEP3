using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEP3Library.Models;
using SEP3Library.UIModels;
using SEP3WebAPI.Data;

namespace SEP3WebAPI.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase {
        private IRestService service;

        public OrdersController(IRestService service) {
            this.service = service;
        }

        [HttpPost]
        // Endpoint = /orders
        public async Task<ActionResult> CreateOrderAsync([FromBody] OrderModel orderModel) {
            try {
                Order newOrder = await service.CreateOrderAsync(orderModel);
                return Created($"/{newOrder.Id}", newOrder);
            }
            catch (InvalidDataException e) {
                return BadRequest(e.Message);
            }
            catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IList<Order>>> GetOrdersAsync([FromQuery] int index) {
            try {
                IList<Order> orders = await service.GetOrdersAsync(index);
                return Ok(orders);
            }
            catch (NullReferenceException e) {
                return NotFound(e.Message);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }
    }
}