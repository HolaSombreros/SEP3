package mediator.message;

import model.Customer;
import model.Order;

import java.util.ArrayList;
import java.util.List;

public class CustomerMessage extends Message {
    private int index;
    private Customer customer;
    private List<Customer> customers;
    private List<Order> orders;
    private int customerId;


    public CustomerMessage(String service, String type) {
        super(service, type);
        customers = new ArrayList<>();
        orders = new ArrayList<>();
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

    public Customer getCustomer() {
        return customer;
    }

    public void setCustomer(Customer customer) {
        this.customer = customer;
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
}
