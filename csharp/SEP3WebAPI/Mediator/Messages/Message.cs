namespace SEP3WebAPI.Mediator.Messages {
    public class Message {
        public string Service { get; set; }
        public string Type { get; set; }

         public Message(string service) {
            Service = service;
        }
    }
}