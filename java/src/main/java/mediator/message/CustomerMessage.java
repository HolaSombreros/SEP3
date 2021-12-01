package mediator.message;

import model.Customer;

import java.util.ArrayList;
import java.util.List;

public class CustomerMessage extends Message {
    private int index;
    private Customer customer;
    private List<Customer> customers;

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

    public Customer getCustomer() {
        return customer;
    }

    public void setCustomer(Customer customer) {
        this.customer = customer;
    }
}
