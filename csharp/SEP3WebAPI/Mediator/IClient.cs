using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;

namespace SEP3WebAPI.Mediator {
    public interface IClient : IItemClient, IOrderClient, ICustomerClient, IFAQClient {
    }
}