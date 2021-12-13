namespace SEP3WebAPI.Mediator.Messages {
    public class ErrorMessage : Message {
        public string Message { get; set; }

        public ErrorMessage() : base("error") {
        }
    }
}