namespace SEP3Library.Model {
    public class Book : Item {
        public string Isbn { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public MyDateTime PublishingYear { get; set; }
        public Language Language { get; set; }
        public Genre Genre { get; set; }
    }
}