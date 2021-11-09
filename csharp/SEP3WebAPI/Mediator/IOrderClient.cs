using System.Threading.Tasks;
using SEP3Library.Model;

namespace SEP3WebAPI.Mediator {
    public interface IOrderClient {
        public Task<Order> CreateOrderAsync(Order order);

    }
}