using System.Collections.Generic;
using SEP3Library.Models;

namespace SEP3WebAPI.Mediator.Messages {
    public class ItemMessage : Message {
        public int Index { get; set; }
        public Item Item { get; set; }
        public Book Book { get; set; }
        public IList<Item> Items { get; set; }
        public IList<Category> Categories { get; set; }
        public IList<Genre> Genres { get; set; }
        public Customer Customer { get; set; }
        public int[] ItemsIds { get; set; }
        public string PriceOrder { get; set; }
        public IList<Review> Reviews { get; set; }
        public string RatingOrder { get; set; }
        public Review Review { get; set; }
        public double AverageRating { get; set; }
        public string DiscountOrder { get; set; }
        public string StatusOrder { get; set; }

        public ItemMessage() : base("item") {
        }
    }
}