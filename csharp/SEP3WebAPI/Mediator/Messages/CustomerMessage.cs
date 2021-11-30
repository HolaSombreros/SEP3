using SEP3Library.Models;

namespace SEP3WebAPI.Mediator.Messages {
    public class CustomerMessage : Message {
        public Customer Customer { get; set; }
    }
}