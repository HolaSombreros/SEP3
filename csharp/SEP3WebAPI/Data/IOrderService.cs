using System.Threading.Tasks;

namespace SEP3WebAPI.Data {
    public interface IOrderService {
        Task<Order> CreateOrderAsync(Order order);
    }
}