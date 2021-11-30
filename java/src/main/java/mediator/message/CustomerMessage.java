package mediator.message;

import model.Customer;

public class CustomerMessage extends Message {

    private Customer customer;

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
