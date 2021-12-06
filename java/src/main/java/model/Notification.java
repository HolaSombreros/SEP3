package model;

public class Notification {
    private int id;
    private String text;
    private MyDateTime time;
    private String status;

    public Notification(int id, String text, MyDateTime time, String status) {
        this.id = id;
        this.text = text;
        this.time = time;
        this.status = status;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getText() {
        return text;
    }

    public void setText(String text) {
        this.text = text;
    }

    public MyDateTime getTime() {
        return time;
    }

    public void setTime(MyDateTime time) {
        this.time = time;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }
}
