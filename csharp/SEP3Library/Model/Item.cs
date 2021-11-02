namespace SEP3Library.Model {
    public class Item {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Category Category { get; set; }
        public double Discount { get; set; }
        public int Quantity { get; set; }
        public ItemStatus Status { get; set; }
        public Review Review { get; set; }
        public string ImageName { get; set; }
    }
}