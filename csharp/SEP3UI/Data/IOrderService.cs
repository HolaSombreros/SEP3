using System.Threading.Tasks;
using SEP3Library.Model;

namespace SEP3UI.Data {
    public interface IOrderService {
        Task<Order> CreateOrderAsync(OrderModel order);
    }
}