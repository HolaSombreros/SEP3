using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEP3WebAPI.Mediator {
    public interface IClient : IItemClient, IOrderClient, ICustomerClient {
    }
}