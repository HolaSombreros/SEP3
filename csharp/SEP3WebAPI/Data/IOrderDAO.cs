using System.Threading.Tasks;
using SEP3Library.Model;
using SEP3Library.UIModels;

namespace SEP3WebAPI.Data {
    public interface IOrderDAO {
        Task<Order> CreateOrderAsync(OrderModel orderModel);
    }
}