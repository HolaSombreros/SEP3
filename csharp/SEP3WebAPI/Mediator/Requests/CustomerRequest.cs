using SEP3Library.Models;

namespace SEP3WebAPI.Mediator.Requests {
    public class CustomerRequest : Request {
        public Customer Customer { get; set; }
    }
}