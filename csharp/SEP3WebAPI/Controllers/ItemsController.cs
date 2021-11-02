using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEP3Library.Model;
using SEP3WebAPI.Data;

namespace SEP3WebAPI.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase {
        private IModelService modelService;

        public ItemsController(IModelService modelService) {
            this.modelService = modelService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IList<Item>>> GetItemsAsync() {
            try {
                IList<Item> items = await modelService.GetItemsAsync();
                return Ok(items);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpPost]
        public async Task<ActionResult> CreateOrder([FromBody] Order order) {
            try {
                Order newOrder = await modelService.CreateOrderAsync(order);
                return Created($"/{newOrder.Id}", newOrder);
            } catch (InvalidDataException e) {
                return BadRequest(e.Message);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }
    }
}