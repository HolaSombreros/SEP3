using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SEP3UI.Model;
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
            if (order.Items.Count < 1) throw new InvalidDataException("Your order must contain at least 1 item");

            return await client.CreateOrderAsync(order);
        }
    }
}