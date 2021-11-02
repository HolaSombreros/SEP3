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
        private IRestService service;

        public ItemsController(IRestService service) {
            this.service = service;
        }
        
        [HttpGet]
        // Endpoint = /items
        public async Task<ActionResult<IList<Item>>> GetItemsAsync() {
            try {
                IList<Item> items = await service.GetItemsAsync();
                return Ok(items);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }
    }
}