package mediator.message;

public class Message {
    private String service;
    private String type;

    public Message(String service, String type) {
        this.service = service;
        this.type = type;
    }

    public String getService() {
        return service;
    }

    public void setService(String service) {
        this.service = service;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }
}
