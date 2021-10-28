using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SEP3UI.Model;
using SEP3WebAPI.Mediator;

namespace SEP3WebAPI.Data {
    public class RestService : IRestService {
        private Client client;
        // TODO - Should we implement a queuing system either here or in the "Client" class to keep track of the order of requests?
        // To ensure each response reaches the correct client
        
        public RestService() {
            client = new Client();
        }
        
        public async Task<IList<Item>> GetItemsAsync() {
            return await client.GetItemsAsync();
        }
        
        public async Task<Order> CreateOrderAsync(Order order) {
            if (order == null) throw new InvalidDataException();
            if (order.Items.Count < 1) throw new InvalidDataException("Your order must contain at least 1 item");
            
            // TODO - I guess we should validate here using the same validation model as we did in the UI to follow the standard principles
            // Same with the 3rd tier. Technically we should probably validate it there too before putting it into the database, or is this
            // just overdoing it?
            
            return await client.CreateOrderAsync(order);
        }
    }
}