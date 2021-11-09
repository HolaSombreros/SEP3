using System.Collections.Generic;
using SEP3Library.Model;

namespace SEP3WebAPI.Mediator.Requests {
    public class ItemRequest : Request {
        public Item Item { get; set; }
        public Book Book { get; set; }
        public IList<Item> Items { get; set; }
    }
}