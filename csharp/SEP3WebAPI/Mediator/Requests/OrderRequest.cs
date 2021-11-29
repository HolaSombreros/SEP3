using System.Collections;
using System.Collections.Generic;
using SEP3Library.Models;

namespace SEP3WebAPI.Mediator.Requests {
    public class OrderRequest : Request {
        public int Index { get; set; }
        public Order Order { get; set; }
        public IList<Order> Orders { get; set; }
    }
}