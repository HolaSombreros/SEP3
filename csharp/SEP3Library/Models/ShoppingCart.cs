using System.Collections.Generic;
using System.Linq;

namespace SEP3Library.Models {
    public class ShoppingCart {
        public readonly decimal ShippingPrice = 25.00M;
        public IList<Item> Items { get; set; } = new List<Item>();

        public decimal Total => Items.Sum(i => i.PriceIncludingDiscount * i.Quantity);
        public decimal TotalOrderPrice => Total + ShippingPrice;
        public int FinalQuantity => Items.Sum(i => i.Quantity);

        public Item Add(Item item) {
            Item exists = Items.FirstOrDefault(i => i.Id == item.Id);
            
            if (exists == null) {
                Item copy = item.Copy();
                copy.Quantity = 1;
                Items.Add(copy);
                return copy;
            } else {
                exists.Quantity++;
                return exists;
            }
        }

        public Item Remove(Item item) {
            Item exists = Items.FirstOrDefault(i => i.Id == item.Id);
            
            if (exists != null) {
                if (exists.Quantity > 1) {
                    exists.Quantity--;
                } else {
                    Items.Remove(exists);
                }
            }

            return exists;
        }

        public void RemoveAll(Item item) {
            Item exists = Items.FirstOrDefault(i => i.Id == item.Id);
            
            Items.Remove(exists);
        }

        public void Clear() {
            Items.Clear();
        }
    }
}