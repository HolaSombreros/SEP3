package mediator.request;

public class Request {
    private String service;
    private String type;

    public Request(String service, String type) {
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
