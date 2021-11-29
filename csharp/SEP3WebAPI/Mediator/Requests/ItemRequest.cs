using System.Collections.Generic;
using SEP3Library.Models;

namespace SEP3WebAPI.Mediator.Requests {
    public class ItemRequest : Request {
        public int Index { get; set; }
        public Item Item { get; set; }
        public Book Book { get; set; }
        public IList<Item> Items { get; set; }
        public IList<Category> Categories { get; set; }
        public IList<Genre> Genres { get; set; }
        public Customer Customer { get; set; }
        public int[] ItemsIds { get; set; }
    }
}