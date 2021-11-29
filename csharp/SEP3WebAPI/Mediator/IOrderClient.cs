using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;

namespace SEP3WebAPI.Mediator {
    public interface IOrderClient {
        public Task<Order> CreateOrderAsync(Order order);
        public Task<IList<Order>> GetOrdersAsync(int index);
    }
}