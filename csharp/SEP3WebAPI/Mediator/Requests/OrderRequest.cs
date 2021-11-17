using SEP3Library.Models;

namespace SEP3WebAPI.Mediator.Requests {
    public class OrderRequest : Request {
        public Order Order { get; set; }
    }
}