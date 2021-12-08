using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SEP3Library.Models;
using SEP3Library.UIModels;
using SEP3UI.Data;
using SEP3WebAPI.Mediator;

namespace SEP3WebAPI.Data {
    public class OrderService : IOrderService {
        private IOrderClient orderClient;
        private IItemClient itemClient;
        private ICustomerClient customerClient;
        
        public OrderService(IOrderClient orderClient, IItemClient itemClient, ICustomerClient customerClient) {
            this.orderClient = orderClient;
            this.itemClient = itemClient;
            this.customerClient = customerClient;
        }

        
        public async Task<Order> CreateOrderAsync(OrderModel orderModel) {
            if (orderModel == null) throw new InvalidDataException("Please specify an order of the proper format");
            if (orderModel.Items == null || orderModel.Items.Count < 1) throw new InvalidDataException("Your order must contain at least 1 item");
            if (!new EmailAddressAttribute().IsValid(orderModel.Email)) throw new InvalidDataException("Please enter a valid email address");
            int[] itemIds = new int[orderModel.Items.Count];
            for (int i = 0; i < itemIds.Length; i++) {
                itemIds[i] = orderModel.Items[i].Id;
            }
            
            IList<Item> items = await itemClient.GetItemsByIdAsync(itemIds);
            orderModel.Items = orderModel.Items.OrderBy(o => o.Id).ToList();

            for (int i = 0; i < orderModel.Items.Count; i++) {
                if (orderModel.Items[i].Quantity > items[i].Quantity) {
                    if (items[i].Quantity == 0)
                        throw new InvalidDataException(
                            $"Item {orderModel.Items[i].Name} is out of stock. The stock will be updated later");

                    throw new InvalidDataException(
                        $"Item {orderModel.Items[i].Name} amount exceeds the amount available. Only this amount is available {items[i].Quantity}");
                }

                if (items[i].Quantity > 50 && items[i].Quantity - orderModel.Items[i].Quantity <= 50) {
                    Notification notification = new Notification() {
                        Text = $"The Item {items[i].Name} is low on stock",
                        Status = "Unread",
                        Time = new MyDateTime() {
                            Year = DateTime.Now.Year,
                            Month = DateTime.Now.Month,
                            Day = DateTime.Now.Day,
                            Hour = DateTime.Now.Hour,
                            Minute = DateTime.Now.Minute,
                            Second = DateTime.Now.Second
                        }
                    };
                    foreach (Customer customer in await customerClient.GetAdminsAsync()) {
                        await customerClient.SendNotificationAsync(customer, notification);
                    }
                }
            }

            Order order = new Order() {
                FirstName = orderModel.FirstName,
                LastName = orderModel.LastName,
                Email = orderModel.Email,
                Address = new Address() {
                    Street = orderModel.Street,
                    Number = orderModel.Number,
                    City = orderModel.City,
                    ZipCode = orderModel.ZipCode
                },
                DateTime = new MyDateTime() {
                    Year = DateTime.Now.Year,
                    Month = DateTime.Now.Month,
                    Day = DateTime.Now.Day,
                    Hour = DateTime.Now.Hour,
                    Minute = DateTime.Now.Minute,
                    Second = DateTime.Now.Second
                },
                Items = orderModel.Items,
                OrderStatus = OrderStatus.Pending,
                CustomerId = orderModel.CustomerId
            };

            return await orderClient.CreateOrderAsync(order);
        }

        public async Task<IList<Order>> GetOrdersAsync(int index, int id, string status) {
            return await orderClient.GetOrdersAsync(index, id, status);
        }

        public async Task<Order> GetOrderAsync(int orderId) {
            return await orderClient.GetOrderAsync(orderId);
        }

     
        public async Task<IList<Order>> GetOrdersByCustomerAsync(int customerId, int index) {
            return await orderClient.GetOrdersByCustomerAsync(customerId, index);
        }

        public async Task UpdateOrderItemsAsync(Order order) {
           await orderClient.UpdateOrderItemsAsync(order);
        }
        
        
        public async Task<Order> UpdateOrderAsync(UpdateOrderModel orderModel) {
            if (orderModel == null) throw new InvalidDataException("Please specify an order of the proper format");
            if (!new EmailAddressAttribute().IsValid(orderModel.Email)) throw new InvalidDataException("Please enter a valid email address");
            Order order = new Order() {
                FirstName = orderModel.FirstName,
                LastName = orderModel.LastName,
                Email = orderModel.Email,
                Address = new Address() {
                    Street = orderModel.Street,
                    Number = orderModel.Number,
                    City = orderModel.City,
                    ZipCode = orderModel.ZipCode
                },
                OrderStatus = orderModel.OrderStatus,
                CustomerId = orderModel.CustomerId,
                Id = orderModel.OrderId
            };
            return await orderClient.UpdateOrderAsync(order);
        }
    }
}