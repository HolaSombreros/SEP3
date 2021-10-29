﻿using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEP3UI.Model;
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
        public async Task<ActionResult> CreateOrderAsync([FromBody] Order order) {
            try {
                Order newOrder = await service.CreateOrderAsync(order);
                return Created($"/{newOrder.Id}", newOrder);
            } catch (InvalidDataException e) {
                return BadRequest(e.Message);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }
    }
}