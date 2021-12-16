using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SEP3Library.Models {
    public class Item {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public decimal PriceIncludingDiscount => Math.Round(Price * ((decimal) (100 - Discount) / 100), 2, MidpointRounding.ToEven);

        public decimal Price { get; set; }
        
        public Category Category { get; set; }
        
        [Range(0, 100)]
        public int Discount { get; set; }
        
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ItemStatus Status { get; set; }
        
        public List<Review> Reviews { get; set; }
        
        public string FilePath { get; set; }
        
        [JsonIgnore]
        public double AverageRating { get; set; }

        public Item Copy() {
            return new Item (){
                Id = Id,
                Name = Name,
                Description = Description,
                Price = Price,
                Category = Category,
                Discount = Discount,
                Status = Status,
                Reviews = Reviews,
                FilePath = FilePath,
                Quantity = Quantity
            };
        }
    }
}