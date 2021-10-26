using System.Collections.Generic;
using SEP3UI.Model;

namespace SEP3WebAPI.Mediator {
    public class Request {
        public string Type { get; set; }
        public IList<Item> Items { get; set; }
        public Order Order { get; set; }
    }
}