namespace SEP3Library.Models {
    public class Notification {
        public int Id { get; set; }
        public string Text { get; set; }
        public MyDateTime Time { get; set; }
        public string Status { get; set; }
    }
}