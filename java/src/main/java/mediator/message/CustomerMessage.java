package mediator.message;

import model.Customer;
import model.Order;
import model.Notification;

import java.util.List;

public class CustomerMessage extends Message {

    private Customer customer;
    private List<Customer> customers;
    private Notification notification;
    private List<Notification> notifications;
    private List<Order> orders;
    private int index;
    private int customerId;


    public List<Order> getOrders() {
        return orders;
    }

    public void setOrders(List<Order> orders) {
        this.orders = orders;
    }

    public int getCustomerId() {
        return customerId;
    }

    public void setCustomerId(int customerId) {
        this.customerId = customerId;
    }
    private List<Customer> customers;
    private Notification notification;
    private List<Notification> notifications;
    private int index;

    public CustomerMessage(String service, String type) {
        super(service, type);
    }

    public Customer getCustomer() {
        return customer;
    }

    public void setCustomer(Customer customer) {
        this.customer = customer;
    }

    public List<Notification> getNotifications() {
        return notifications;
    }

    public void setNotifications(List<Notification> notifications) {
        this.notifications = notifications;
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

    public Notification getNotification() {
        return notification;
    }

    public void setNotification(Notification notification) {
        this.notification = notification;
    }
}
