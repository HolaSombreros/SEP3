namespace SEP3Library.Models {
    public class Review {
        public Customer Customer { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int ItemId { get; set; }
        public MyDateTime DateTime {get; set; }
    }
}