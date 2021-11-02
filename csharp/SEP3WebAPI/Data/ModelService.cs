using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Model;
using SEP3WebAPI.Mediator;

namespace SEP3WebAPI.Data {
    public class ModelService : IModelService {
        private Client client;
        // Might need a queue here to handle multiple clients at once.
        
        public ModelService() {
            client = new Client();
        }
        
        public async Task<IList<Item>> GetItemsAsync() {
            return await client.GetItemsAsync();
        }
        
        public async Task<Order> CreateOrderAsync(Order order) {
            // TODO - Validate order
            return await client.CreateOrderAsync(order);
        }
    }
}