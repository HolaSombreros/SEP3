namespace SEP3WebAPI.Mediator.Messages {
    public class Message {
        public string Service { get; set; }
        public string Type { get; set; }

        /**
         * The service is set automatically when instantiating the specific classes
         */
         public Message(string service) {
            Service = service;
        }
    }
}