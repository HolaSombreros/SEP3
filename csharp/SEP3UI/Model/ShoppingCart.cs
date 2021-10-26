using System.Collections.Generic;

namespace SEP3UI.Model {
    public class ShoppingCart {
        public User User { get; set; }
        public double Total { get; set; }
        public IList<Item> Items { get; set; }
    }
}