
using System.Collections.Generic;
using SEP3Library.Models;

namespace SEP3WebAPI.Mediator.Messages {
    public class CustomerMessage : Message {
        public Customer Customer { get; set; }
        public IList<Customer> Customers { get; set; }
        public Notification Notification { get; set; }
        public IList<Notification> Notifications { get; set; }
        public int Index { get; set; }
        public int ItemId { get; set; }
    }
}