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
            IList<Item> items = await GetAsync<IList<Item>>("items");
            return items;
        }
        
        public async Task<Order> CreateOrderAsync(OrderModel orderModel) {
            Order newOrder = await PostAsync<OrderModel, Order>(orderModel, "orders");
            return newOrder;
        }
        
        /**
         * <summary>Method to send an asynchronous GET request to a specific endpoint</summary>
         * <remarks>Might need refactoring at some point to allow for x2 T in the case we want
         * all 'children' and they inhering from 'people' like in the DNP assignment... Maybe?</remarks>
         */
        private async Task<T> GetAsync<T>(string endpoint) {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{uri}/{endpoint}");
            string responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new Exception($"{responseContent}");
            T obj = JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions() {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            
            return obj;
        }
        
        /**
         * <summary>Method to send an asynchronous POST request with an object body to a specific endpoint</summary>
         * <returns>Returns an object of a specific type</returns>
         */
        private async Task<TOutput> PostAsync<TInput, TOutput>(TInput obj, string endpoint) {
            string json = JsonSerializer.Serialize(obj);
            using HttpClient client = new HttpClient();
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync($"{uri}/{endpoint}", content);
            string responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new Exception($"{responseContent}");

            TOutput created = JsonSerializer.Deserialize<TOutput>(responseContent, new JsonSerializerOptions() {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            
            return created;
        }
    }
}