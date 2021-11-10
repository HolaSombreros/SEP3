package mediator.Request;

public class ErrorRequest extends Request {
    private String message;

    public ErrorRequest(String service, String type) {
        super(service, type);
    }

    public String getMessage() {
        return message;
    }

    public void setMessage(String message) {
        this.message = message;
    }
}
