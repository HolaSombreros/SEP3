using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SEP3UI.Data {
    public static class RestService {
        private const string uri = "https://localhost:5003";

        /**
         * <summary>Method to send an asynchronous GET request to a specific endpoint</summary>
         * <returns>Returns an object of a specific type</returns>
         */
        public static async Task<T> GetAsync<T>(string endpoint) {
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
        public static async Task<TOutput> PostAsync<TInput, TOutput>(TInput obj, string endpoint) {
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