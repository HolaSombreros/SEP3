using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SEP3Library.Models {
    public class Item {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public double Price {
            get => price - price * ((double) Discount / 100);
            set => price = value;
        }
        private double price;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Category Category { get; set; }
        [Range(0, 100)]
        public int Discount { get; set; }
        public int Quantity { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ItemStatus Status { get; set; }
        
        public Review Review { get; set; }
        
        public string ImageName { get; set; }

        public Item Copy() {
            return new Item (){
                Id = Id,
                Name = Name,
                Description = Description,
                Price = Price,
                Category = Category,
                Discount = Discount,
                Status = Status,
                Review = Review,
                ImageName = ImageName,
                Quantity = Quantity
            };
        }
    }
}