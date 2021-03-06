using System.Threading.Tasks;

namespace SEP3UI.Data {
    public interface IRestService {
        Task<T> GetAsync<T>(string endpoint);
        Task<TOutput> PostAsync<TInput, TOutput>(TInput obj, string endpoint);
        Task<TOutput> PutAsync<TInput, TOutput>(TInput obj, string endpoint);
        Task DeleteAsync(string endpoint);
    }
}