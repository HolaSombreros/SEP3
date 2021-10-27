using System.Threading.Tasks;
using SEP3UI.Model;

namespace SEP3UI.Data {
    public interface IOrderService {
        Task<Order> CreateOrderAsync(Order order);
    }
}