using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SEP3UI.Model;

namespace SEP3UI.Data {
    public class ModelService : IModelService {
        private const string uri = "https://localhost:5003";
        private HttpClient client;

        public ModelService() {
            client = new HttpClient();
        }

        public async Task<IList<Item>> GetItemsAsync() {
            HttpResponseMessage response = await client.GetAsync(uri + "/items");
            if (!response.IsSuccessStatusCode) {
                throw new Exception(response.ToString());
            }

            IList<Item> items = JsonSerializer.Deserialize<List<Item>>(response.ToString());
            return items;
        }

        public async Task<Order> CreateOrderAsync(Order order) {
            string json = JsonSerializer.Serialize(order);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            
            HttpResponseMessage response = await client.PostAsync(uri + "/orders", content);
            if (!response.IsSuccessStatusCode) {
                throw new Exception(response.ToString());
            }
            
            Order newOrder = JsonSerializer.Deserialize<Order>(response.ToString());
            return newOrder;
        }
    }
}