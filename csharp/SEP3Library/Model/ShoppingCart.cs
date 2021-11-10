using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Schema;

namespace SEP3Library.Model {
    public class ShoppingCart {
        public Customer User { get; set; }
        public readonly double ShippingPrice = 25.00;
        public IList<Item> Items  { get; set; }
        
        public ShoppingCart() {
            Items = new List<Item>();
        }

        public double Total {
            get {
                double p = 0;
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

        public void RemoveFromShoppingCart(Item item) {
            Item i = item.Copy();
            Item test = Items.First(it => it.Id == item.Id);
            test.Quantity--;
        }
        
        public void EmptyShoppingCart() {
            Items.Clear();
        }
        
        public double TotalOrderPrice() {
            return Total + ShippingPrice;
        }
    }
}