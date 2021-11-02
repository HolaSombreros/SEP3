using System.Text.Json.Serialization;

namespace SEP3Library.Model {
    public class Item {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Category Category { get; set; }
        public int Discount { get; set; }
        public int Quantity { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ItemStatus Status { get; set; }
        
        public Review Review { get; set; }
        
        public string ImageName { get; set; }
    }
}