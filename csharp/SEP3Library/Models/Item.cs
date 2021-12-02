using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SEP3Library.Models {
    public class Item {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public decimal PriceIncludingDiscount => Math.Round(price * ((decimal) (100 - Discount) / 100), 2, MidpointRounding.ToEven);

        public decimal Price {
            get => price;
            set => price = value;
        }
        private decimal price;
        
        public Category Category { get; set; }
        
        [Range(0, 100)]
        public int Discount { get; set; }
        
        [Range(0, Int32.MaxValue)]
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