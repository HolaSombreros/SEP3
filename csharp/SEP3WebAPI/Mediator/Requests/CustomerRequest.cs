using SEP3Library.Model;

namespace SEP3WebAPI.Mediator.Requests {
    public class CustomerRequest : Request {
        public Customer Customer { get; set; }
    }
}