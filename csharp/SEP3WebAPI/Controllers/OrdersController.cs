using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEP3Library.Models;
using SEP3Library.UIModels;
using SEP3WebAPI.Data;

namespace SEP3WebAPI.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase {
        private IOrderService service;

        public OrdersController(IOrderService service) {
            this.service = service;
        }

        [HttpPost]
        // Endpoint = /orders
        public async Task<ActionResult> CreateOrderAsync([FromBody] OrderModel orderModel) {
            try {
                Order newOrder = await service.CreateOrderAsync(orderModel);
                return Created($"/{newOrder.Id}", newOrder);
            } catch (InvalidDataException e) {
                return BadRequest(e.Message);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IList<Order>>> GetOrdersAsync([FromQuery] int index, [FromQuery] int id, [FromQuery] string status) {
            try {
                IList<Order> orders = await service.GetOrdersAsync(index, id, status);
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

        [HttpGet]
        [Route("{orderId:int}")]
        public async Task<ActionResult<Order>> GetOrderAsync([FromRoute] int orderId) {
            try {
                Order order = await service.GetOrderAsync(orderId);
                if (order == null) {
                    return NotFound($"No order found with id {orderId}");
                }

                return Ok(order);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        [Route("{customerId:int}/{orderId:int}")]
        public async Task<ActionResult<Order>> UpdateOrderAsync(UpdateOrderModel updateOrderModel) {
            try {
                Order order = await service.UpdateOrderAsync(updateOrderModel);
                return Ok(order);
            }
            catch (NullReferenceException e) {
                return NotFound(e.Message);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpPut]
        [Route("{orderId:int}")]
        public async Task<ActionResult<Order>> ReturnItemsAsync([FromBody] ReturnItemsModel model, [FromRoute] int orderId) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            
            if (model.Items.All(i => i.Quantity < 1 )) {
                return BadRequest("Please specify the items you want to return");
            }
            
            try {
                Order order = await service.GetOrderAsync(orderId);
                if (order == null) {
                    return NotFound($"No such order found with id: {model.OrderId}");
                }

                for (int i = 0; i < model.Items.Count; i++) {
                    if (order.Items[i].Quantity < model.Items[i].Quantity) {
                        throw new Exception("Please specify a proper quantity of the items you want to return");
                    }
                    
                    order.Items[i].Quantity = model.Items[i].Quantity;
                }

                await service.UpdateOrderItemsAsync(order);
                return Ok(order);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }
    }
}