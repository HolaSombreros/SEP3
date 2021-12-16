package mediator.message;

import model.Customer;
import model.Notification;

import java.util.ArrayList;
import java.util.List;

public class CustomerMessage extends Message {
    private int index;
    private int itemId;
    private List<Customer> customers;
    private List<Notification> notifications;


    public CustomerMessage(String service, String type) {
        super(service, type);
        customers = new ArrayList<>();
    }

    public int getIndex() {
        return index;
    }

    public void setIndex(int index) {
        this.index = index;
    }

    public List<Customer> getCustomers() {
        return customers;
    }

    public void setCustomers(List<Customer> customers) {
        this.customers = customers;
    }

    public List<Notification> getNotifications() {
        return notifications;
    }

    public void setNotifications(List<Notification> notifications) {
        this.notifications = notifications;
    }

    public int getItemId() {
        return itemId;
    }

    public void setItemId(int itemId) {
        this.itemId = itemId;
    }
}
