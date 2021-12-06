using System.Collections.Generic;
using SEP3Library.Models;

namespace SEP3WebAPI.Mediator.Messages {
    public class OrderMessage : Message {
        public int Index { get; set; }
        public Order Order { get; set; }
        public IList<Order> Orders { get; set; }
        public int CustomerId { get; set; }
        public string Status { get; set; }
    }
}