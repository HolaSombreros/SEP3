using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SEP3UI.Model;

namespace SEP3UI.Data {
    public class RestService : IModelService {
        private const string uri = "https://localhost:5003";
        private enum HttpRequest {
            POST,
            PUT
        }
        
        public ShoppingCart ShoppingCart { get; init; }
        
        public RestService() {
            ShoppingCart = new ShoppingCart();
        }
        
        public async Task<IList<Item>> GetItemsAsync() {
            IList<Item> items = await MakeHttpRequestAsync<List<Item>>("/items");
            return items;
        }
        
        public async Task<Order> CreateOrderAsync(Order order) {
            Order newOrder = await MakeHttpRequestAsync(order, HttpRequest.POST, "/orders");
            return newOrder;
        }
        
        // Method for a GET request
        private async Task<T> MakeHttpRequestAsync<T>(string route) {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{uri}{route}");
            if (!response.IsSuccessStatusCode) throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            
            string json = await response.Content.ReadAsStringAsync();
            T obj = JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions() {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            
            return obj;
        }
        
        // Method for a POST or a PUT request
        private async Task<T> MakeHttpRequestAsync<T>(T bodyObject, HttpRequest type, string route) {
            string json = JsonSerializer.Serialize(bodyObject);
            using HttpClient client = new HttpClient();
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response;
            
            switch (type) {
                case HttpRequest.POST:
                    response = await client.PostAsync($"{uri}{route}", content);
                    break;
                case HttpRequest.PUT:
                    response = await client.PutAsync($"{uri}{route}", content);
                    break;
                default:
                    throw new Exception("Illegal HTTP request type detected. Allowed types: POST, PUT");
            }

            if (!response.IsSuccessStatusCode) throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            
            json = await response.Content.ReadAsStringAsync();
            T newObject = JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions() {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            
            return newObject;
        }
        
        // Method for a DELETE request
        private async Task MakeHttpRequestAsync(string route) {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{uri}{route}");
            if (!response.IsSuccessStatusCode) throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
        }
    }
}