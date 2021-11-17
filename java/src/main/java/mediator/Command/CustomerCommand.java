package mediator.Command;

import database.daomodel.DatabaseManager;
import mediator.Request.*;
import model.Customer;

import java.util.HashMap;

public class CustomerCommand implements Command {

    private CustomerRequest request;
    private CustomerRequest reply;
    private DatabaseManager databaseManager;
    private HashMap<String, Runnable> methods;

    public CustomerCommand(DatabaseManager databaseManager) {
        this.databaseManager = databaseManager;
        methods = new HashMap<>();
        methods.put("get", this::get);
        methods.put("login", this::login);
        methods.put("register", this::register);
    }

    @Override public Request execute(Request request) {
        try {
            this.request = (CustomerRequest) request;
            reply = new CustomerRequest(request.getService(), request.getType());
            methods.get(request.getType()).run();
            return reply;
        }
        catch (Exception e) {
            e.printStackTrace();
            throw new IllegalArgumentException("The request could not be fulfilled");
        }
    }

    private void get() {
        reply.setCustomer(databaseManager.getCustomerDAOService().read(request.getCustomer().getId()));
    }

    private void login() {
        reply.setCustomer(databaseManager.getCustomerDAOService().read(request.getCustomer().getEmail()));
        if (!request.getCustomer().getPassword().equals(reply.getCustomer().getPassword()))
            throw new IllegalArgumentException("Invalid password");
    }

    private void register() {
        Customer customer = request.getCustomer();
        reply.setCustomer(databaseManager.getCustomerDAOService().create(customer.getFirstName(), customer.getLastName(), customer.getEmail(), customer.getPassword(),
            customer.getRole(), customer.getAddress(), customer.getPhoneNumber()));
    }
}
