using System.Collections.Generic;
using System.Linq;

namespace SEP3Library.Models {
    public class ShoppingCart {
        public readonly decimal ShippingPrice = 25.00M;
        public IList<Item> Items { get; set; }
        
        public ShoppingCart() {
            Items = new List<Item>();
        }

        public decimal Total {
            get {
                decimal p = 0;
                foreach (var i in Items) {
                    p += i.Price * i.Quantity;
                }
                return p;
            } 
        }

        public int FinalQuantity {
            get {
                int q = 0;
                foreach (var i in Items) {
                    q += i.Quantity;
                }
                return q;
            }
        }

        public Item IncreaseQuantity(Item item) {
            Item i = item.Copy();
            Item test = Items.First(it => it.Id == item.Id);
            test.Quantity++;
            return test;
        }

        public Item AddToShoppingCart(Item item) {
            Item item1 = item.Copy();
            Item test = Items.FirstOrDefault(i => i.Id == item.Id);
            if (test == null) {
                item1.Quantity = 1;
                Items.Add(item1);
                return item1;
            }
            return null;
        }

        public Item RemoveQuantityFromShoppingCart(Item item) {
            Item i = item.Copy();
            Item test = Items.First(it => it.Id == item.Id);
            test.Quantity--;
            return test;
        }

        public void RemoveItemFromShoppingCart(Item item) {
            Items.Remove(item);
        }
        
        
        public void EmptyShoppingCart() {
            Items.Clear();
        }
        
        public decimal TotalOrderPrice() {
            return Total + ShippingPrice;
        }
    }
}