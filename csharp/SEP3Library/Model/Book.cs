using System.Text.Json.Serialization;

namespace SEP3Library.Model {
    public class Book : Item {
        public string Isbn { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public MyDateTime PublicationDate { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Language Language { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Genre Genre { get; set; }
    }
}