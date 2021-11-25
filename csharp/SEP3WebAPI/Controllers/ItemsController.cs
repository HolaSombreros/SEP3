using System;
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
    public class ItemsController : ControllerBase {
        private IRestService service;

        public ItemsController(IRestService service) {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Item>>> GetItemsAsync([FromQuery] int index, [FromQuery] string? searchName) {
            try {
                if (searchName == null) {
                    IList<Item> items = await service.GetItemsAsync(index);
                    return Ok(items);
                }
                else {
                    IList<Item> items = await service.GetItemsBySearchAsync(searchName,index);
                    return Ok(items);
                }
                
            } 
            catch (NullReferenceException e) {
                return NotFound(e.Message);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Item>> GetItemAsync([FromRoute] int id) {
            try {
                Item item = await service.GetItemAsync(id);
                return Ok(item);
            }
            catch (NullReferenceException e) {
                return NotFound(e.Message);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpGet]
        [Route("Books/{id:int}")]
        public async Task<ActionResult<Book>> GetBookAsync([FromRoute] int id) {
            try {
                Item item = await service.GetBookAsync(id);
                return Ok(item);
            }
            catch (NullReferenceException e) {
                return NotFound(e.Message);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("categories")]
        public async Task<ActionResult<IList<Category>>> GetCategoriesAsync() {
            try {
                IList<Category> categories = await service.GetCategoriesAsync();
                return Ok(categories);
            }
            catch (NullReferenceException e) {
                return NotFound(e.Message);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("genres")]
        public async Task<ActionResult<IList<Genre>>> GetGenresAsync() {
            try {
                IList<Genre> genres = await service.GetGenresAsync();
                return Ok(genres);
            }
            catch (NullReferenceException e) {
                return NotFound(e.Message);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Item>> AddItemAsync([FromBody] ItemModel itemModel) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try {
                Item item = await service.CreateItemAsync(itemModel);
                return Created($"/{item.Id}", item);
            }
            catch (InvalidDataException e) {
                return BadRequest(e.Message);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }
    }
}