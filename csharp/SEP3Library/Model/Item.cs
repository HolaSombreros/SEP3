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