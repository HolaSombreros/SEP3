using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SEP3Library.Model {
    public class Order {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Please specify the date and time this order was created")]
        [DataType(DataType.DateTime, ErrorMessage = "Please specify a valid date and time of the order")]
        public DateTime DateTime { get; set; }
        
        [Required(ErrorMessage = "Please specify the customer of the order")]
        public Customer User { get; set; }

        [Required(ErrorMessage = "Please specify the items this order consists of")]
        public IList<Item> Items { get; set; }
        
        [Required(ErrorMessage = "Please specify the status of the order")]
        public OrderStatus OrderStatus { get; set; }
    }
}