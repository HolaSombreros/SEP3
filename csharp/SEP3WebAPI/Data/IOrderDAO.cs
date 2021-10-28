using System.Threading.Tasks;
using SEP3UI.Model;

namespace SEP3WebAPI.Data {
    public interface IOrderDAO {
        Task<Order> CreateOrderAsync(Order order);
    }
}