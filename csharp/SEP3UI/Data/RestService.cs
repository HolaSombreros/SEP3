using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SEP3Library.Model;


namespace SEP3UI.Data {
    public class RestService : IModelService {
        private const string uri = "https://localhost:5003";
        
        public ShoppingCart ShoppingCart { get; init; }
        
        public RestService() {
            ShoppingCart = new ShoppingCart();
        }
        
        public async Task<IList<Item>> GetItemsAsync() {
            IList<Item> items = await GetRequest<List<Item>>("items");
            return items;
        }
        
        public async Task<Order> CreateOrderAsync(Order order) {
            Order newOrder = await PostRequest(order, "orders");
            return newOrder;
        }
        
        // Method to send a GET request to a specific endpoint
        private async Task<T> GetRequest<T>(string endpoint) {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{uri}/{endpoint}");
            if (!response.IsSuccessStatusCode) throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            
            string json = await response.Content.ReadAsStringAsync();
            T obj = JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions() {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            
            return obj;
        }
        
        // Method to send a POST request to a specific endpoint
        private async Task<T> PostRequest<T>(T body, string endpoint) {
            string json = JsonSerializer.Serialize(body);
            
            using HttpClient client = new HttpClient();
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"{uri}/{endpoint}", content);
            if (!response.IsSuccessStatusCode) throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            
            json = await response.Content.ReadAsStringAsync();
            T obj = JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions() {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            
            return obj;
        }
        
        // Method to send a PUT request to a specific endpoint
        private async Task<T> PutRequest<T>(T body, string endpoint) {
            string json = JsonSerializer.Serialize(body);
            
            using HttpClient client = new HttpClient();
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"{uri}/{endpoint}", content);
            if (!response.IsSuccessStatusCode) throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            
            json = await response.Content.ReadAsStringAsync();
            T obj = JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions() {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            
            return obj;
        }
        
        // Method to send a DELETE request to a specific endpoint
        private async Task DeleteRequest(string endpoint) {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{uri}/{endpoint}");
            if (!response.IsSuccessStatusCode) throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
        }
    }
}