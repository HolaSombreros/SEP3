using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Model;

namespace SEP3WebAPI.Mediator {
    public interface IClient : IItemClient, IOrderClient, ICustomerClient {
    }
}