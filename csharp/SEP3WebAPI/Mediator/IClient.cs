using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Model;

namespace SEP3WebAPI.Mediator {
    public interface IClient {
        public Task<IList<Item>> GetItemsAsync();
        public Task<Item> GetItemAsync(int id);
        public Task<Order> CreateOrderAsync(Order order);
    }
}