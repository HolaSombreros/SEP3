using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SEP3Library.Models;
using SEP3Library.UIModels;
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

        /**
         * The method checks if there are enough items for completing the order
         * The method calls also SendNotificationAsync to the administrators that a specific item's quantity dropped to 50 meaning low on stock
         * The method creates a new order based on order model
         */
        public async Task<Order> CreateOrderAsync(OrderModel orderModel) {
            if (orderModel == null) throw new InvalidDataException("Please specify an order of the proper format");
            if (orderModel.Items == null || orderModel.Items.Count < 1) throw new InvalidDataException("Your order must contain at least 1 item");
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
                        Time = new MyDateTime(new DateTime())
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
                DateTime = new MyDateTime(new DateTime()), 
                Items = orderModel.Items,
                OrderStatus = OrderStatus.Pending,
                CustomerId = orderModel.CustomerId
            };

            return await orderClient.CreateOrderAsync(order);
        }

        /**
         * The pending orders created 3 days ago are set to finished
         */
        public async Task<IList<Order>> GetOrdersAsync(int index, int id, string status) {
            IList<Order> orders = await orderClient.GetOrdersAsync(index, id, status);
            foreach (Order order in orders) {
                if (order.OrderStatus == OrderStatus.Pending) {
                    DateTime orderDate = new DateTime(order.DateTime.Year, order.DateTime.Month, order.DateTime.Day);
                    if (DateTime.Now.Subtract(orderDate).CompareTo(new TimeSpan(3, 0, 0, 0)) > 0) {
                        order.OrderStatus = OrderStatus.Finished;
                        await orderClient.UpdateOrderAsync(order);
                    }
                }
            }
            return orders;
        }

        public async Task<Order> GetOrderAsync(int orderId) {
            return await orderClient.GetOrderAsync(orderId);
        }

     
        public async Task<IList<Order>> GetOrdersByCustomerAsync(int customerId, int index) {
            IList<Order> orders = await orderClient.GetOrdersByCustomerAsync(customerId, index);
            foreach (Order order in orders) {
                if (order.OrderStatus == OrderStatus.Pending) {
                    DateTime orderDate = new DateTime(order.DateTime.Year, order.DateTime.Month, order.DateTime.Day);
                    if (DateTime.Now.Subtract(orderDate).CompareTo(new TimeSpan(3, 0, 0, 0)) > 0) {
                        order.OrderStatus = OrderStatus.Finished;
                        await orderClient.UpdateOrderAsync(order);
                    }
                }
            }
            return orders;
        }

        public async Task UpdateOrderItemsAsync(Order order) {
           await orderClient.UpdateOrderItemsAsync(order);
        }
        
        /**
         * The method creates a new order based on order model
         */
        public async Task<Order> UpdateOrderAsync(UpdateOrderModel orderModel) {
            if (orderModel == null) 
                throw new InvalidDataException("Please specify an order of the proper format");
            
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