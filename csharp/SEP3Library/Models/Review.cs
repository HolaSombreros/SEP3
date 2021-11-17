namespace SEP3Library.Models {
    public class Review {
        public Customer Customer { get; set; }
        public Rating Rating { get; set; }
        public string Comment { get; set; }
    }
}