package mediator.message;

import model.Customer;
import model.Order;

import java.util.List;

public class CustomerMessage extends Message {

    private Customer customer;
    private List<Order> orders;
    private int index;
    private int customerId;

    public int getIndex() {
        return index;
    }

    public void setIndex(int index) {
        this.index = index;
    }

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

    public CustomerMessage(String service, String type) {
        super(service, type);
    }

    public Customer getCustomer() {
        return customer;
    }

    public void setCustomer(Customer customer) {
        this.customer = customer;
    }
}
