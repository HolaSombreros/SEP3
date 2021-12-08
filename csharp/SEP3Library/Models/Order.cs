using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;

namespace SEP3Library.Models {
    public class Order {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        
        [Required(ErrorMessage = "Please specify the date and time this order was created")]
        public MyDateTime DateTime { get; set; }

        [Required(ErrorMessage = "Please specify your first name")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Please specify your last name")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please specify an email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please specify the address where the order is to be sent to")]
        public Address Address { get; set; }

        [Required(ErrorMessage = "Please specify the items the order consists of")]
        public IList<Item> Items { get; set; }
        
        [Required(ErrorMessage = "Please specify the status of the order")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderStatus OrderStatus { get; set; }

        [JsonIgnore]
        public decimal Total {
            get {
                return Math.Round(Items.Sum(i => i.PriceIncludingDiscount * i.Quantity) + 25.00M, 2, MidpointRounding.ToEven);
            }
        }
    }
}