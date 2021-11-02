using System.Threading.Tasks;
using SEP3Library.Model;


namespace SEP3WebAPI.Data {
    public interface IOrderDAO {
        Task<Order> CreateOrderAsync(Order order);
    }
}