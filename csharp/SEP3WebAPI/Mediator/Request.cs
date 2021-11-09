using System.Collections.Generic;
using SEP3Library.Model;

namespace SEP3WebAPI.Mediator {
    public class Request {
        public string Type { get; set; }
        public Item Item { get; set; } = new Item();
        public IList<Item> Items { get; set; }
        public Order Order { get; set; }
        
        public Customer Customer { get; set; }
    }
}