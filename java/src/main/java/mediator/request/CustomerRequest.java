package mediator.request;

import model.Customer;

public class CustomerRequest extends Request{

    private Customer customer;

    public CustomerRequest(String service, String type) {
        super(service, type);
    }

    public Customer getCustomer() {
        return customer;
    }

    public void setCustomer(Customer customer) {
        this.customer = customer;
    }
}
