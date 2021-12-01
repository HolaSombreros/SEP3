
using System.Collections.Generic;
using SEP3Library.Models;

namespace SEP3WebAPI.Mediator.Messages {
    public class CustomerMessage : Message {
        public Customer Customer { get; set; }
        public List<Customer> Customers { get; set; }
        public IList<Order> Orders { get; set; }
        public int Index { get; set; }
        public int CustomerId { get; set; }

    }
}