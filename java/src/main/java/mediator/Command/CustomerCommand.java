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
        methods.put("update", this::update);
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
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    private void get() {
        reply.setCustomer(databaseManager.getCustomerDAOService().read(request.getCustomer().getId()));
    }

    private void login() {
        reply.setCustomer(databaseManager.getCustomerDAOService().read(request.getCustomer().getEmail()));
    }

    private void register() {
        Customer customer = request.getCustomer();
        reply.setCustomer(databaseManager.getCustomerDAOService().create(customer.getFirstName(), customer.getLastName(), customer.getEmail(), customer.getPassword(),
            customer.getRole(), customer.getAddress(), customer.getPhoneNumber()));
    }

    private void update() {
        Customer customer = request.getCustomer();
        reply.setCustomer(databaseManager.getCustomerDAOService().update(customer));
    }
}
