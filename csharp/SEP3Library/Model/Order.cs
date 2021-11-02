using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SEP3Library.Model {
    public class Order {
        public IList<Item> Items { get; set; }
        
        public User User { get; set; }
        public int Id { get; set; }
        
        public Address Address { get; set; }
        
        public MyDateTime DateTime { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderStatus OrderStatus { get; set; }
    }
}