﻿using System;
using System.Collections.Generic;
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
        public async Task<ActionResult<IList<Item>>> GetItemsAsync() {
            try {
                IList<Item> items = await service.GetItemsAsync();
                
                return Ok(items);
                
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
        public async Task<ActionResult<IList<Item>>> GetItemsById([FromQuery] int[] itemIds) {
            try {
                foreach (var i in itemIds) {
                    Console.WriteLine(i);
                }

                IList<Item> items = await service.GetItemsByIdAsync(itemIds);
                
                return Ok(items);
                
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
    }
}