package mediator.message;

public class ErrorMessage extends Message {
    private String message;

    public ErrorMessage(String service, String type) {
        super(service, type);
    }

    public String getMessage() {
        return message;
    }

    public void setMessage(String message) {
        this.message = message;
    }
}
