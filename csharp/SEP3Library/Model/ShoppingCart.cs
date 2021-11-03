using System.Collections.Generic;
using System.Linq;

namespace SEP3Library.Model {
    public class ShoppingCart {
        public Customer User { get; set; }
        public double Total { get; set; }
        public IList<Item> Items  { get; set; }

        public ShoppingCart() {
            Items = new List<Item>();
        }
        
        public void AddToShoppingCart(Item item) {
            Item item1 = item.Copy();
            Item test = Items.FirstOrDefault(i => i.Id == item.Id);
            if (test == null) {
                Items.Add(item1);
                item1.Quantity = 1;
            }
            else {
                test.Quantity++;
            }
        }
        public void EmptyShoppingCart() {
            Items.Clear();
        }
    }
}