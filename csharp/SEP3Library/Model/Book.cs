namespace SEP3Library.Model {
    public class Book : Item {
        public string Isbn { get; set; }
        public string Author { get; set; }
        public int PublishingYear { get; set; }
        public Language Language { get; set; }
    }
}