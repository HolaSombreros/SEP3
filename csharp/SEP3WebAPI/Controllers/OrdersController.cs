using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEP3Library.Model;
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
            } catch (ArgumentNullException e) {
                return BadRequest(e.Message);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }
    }
}