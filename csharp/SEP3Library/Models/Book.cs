using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SEP3Library.Models {
    public class Book : Item {
        public string Isbn { get; set; }
        public List<Author> Authors { get; set; }
        public MyDateTime PublicationDate { get; set; }
        public string Language { get; set; }
        public List<Genre> Genre { get; set; }
    }
}