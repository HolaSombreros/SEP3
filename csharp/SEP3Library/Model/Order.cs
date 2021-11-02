using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SEP3Library.Model {
    public class Order {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Please specify the date and time the order was created")]
        [DataType(DataType.DateTime, ErrorMessage = "Please specify a validate date and time")]
        public DateTime DateTime { get; set; }
        
        [Required(ErrorMessage = "Please specify the customer")]
        public User User { get; set; }
        
        [Required(ErrorMessage = "Please specify an address to where the order is to be shipped to")]
        public Address Address { get; set; }
        
        [Required]
        [MinLength(1, ErrorMessage = "Your order must contain at least 1 item")]
        public IList<Item> Items { get; set; }
        
        [Required]
        public OrderStatus OrderStatus { get; set; }
    }
}