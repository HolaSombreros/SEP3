using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace SEP3Library.Model {
    public class Order {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please specify the customer")]
        public User User { get; set; }
        [Required(ErrorMessage = "Please specify an address to where the order is to be shipped to")]
        public Address Address { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "Your order must contain at least 1 item")]
        public IList<Item> Items { get; set; }
        [Required]
        public MyDateTime DateTime { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderStatus OrderStatus { get; set; }
    }
}