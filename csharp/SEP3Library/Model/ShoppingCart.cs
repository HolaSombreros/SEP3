using System;
using System.Collections.Generic;

namespace SEP3Library.Model {
    public class ShoppingCart {
        public User User { get; set; }
        public double Total { get; set; }
        public IList<Item> Items  { get; set; }

        public ShoppingCart() {
            Items = new List<Item>();
        }
        
        public void AddToShoppingCart(Item item) {
            item.Quantity++;
            if (!Items.Contains(item)) 
                Items.Add(item);
            Console.WriteLine(Items.Count);
        }
    }
}