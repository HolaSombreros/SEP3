package mediator.command;

import database.daomodel.DatabaseManager;
import mediator.message.*;
import model.Customer;

import java.util.HashMap;

public class CustomerCommand implements Command {
    private CustomerMessage request;
    private CustomerMessage reply;
    private DatabaseManager databaseManager;
    private HashMap<String, Runnable> methods;

    public CustomerCommand(DatabaseManager databaseManager) {
        this.databaseManager = databaseManager;
        methods = new HashMap<>();
        methods.put("getCustomer", this::getCustomer);
        methods.put("login", this::login);
        methods.put("register", this::register);
        methods.put("updateCustomer", this::updateCustomer);
        methods.put("getNotifications", this::getNotifications);
        methods.put("getNotification", this::getNotification);
        methods.put("getAdmins", this::getAdmins);
        methods.put("sendNotification", this::sendNotification);
        methods.put("updateSeenNotification", this:: updateSeenNotification);
        methods.put("getCustomersByIndex", this::getCustomersByIndex);
        methods.put("updateRole", this::updateRole);
        methods.put("customerWithWishlistItem", this::getCustomerWithWishlistItem);
    }

    @Override public Message execute(Message request) {
        try {
            this.request = (CustomerMessage) request;
            reply = new CustomerMessage(request.getService(), request.getType());
            methods.get(request.getType()).run();
            return reply;
        }
        catch (Exception e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    private void getCustomer() {
        reply.getCustomers().add(0,databaseManager.getCustomerDAOService().read(request.getCustomers().get(0).getId()));
    }

    private void login() {
        reply.getCustomers().add(0,databaseManager.getCustomerDAOService().read(request.getCustomers().get(0).getEmail()));
    }

    private void register() {
        Customer customer = request.getCustomers().get(0);
        reply.getCustomers().add(0,databaseManager.getCustomerDAOService()
            .create(customer.getFirstName(), customer.getLastName(), customer.getEmail(), customer.getPassword(), customer.getRole(), customer.getAddress(),
                customer.getPhoneNumber()));
    }

    private void updateCustomer() {
        reply.getCustomers().add(0,databaseManager.getCustomerDAOService().update(request.getCustomers().get(0)));
    }

    private void getCustomersByIndex() {
        reply.setCustomers(databaseManager.getCustomerDAOService().readByIndex(request.getIndex()));
    }

    private void updateRole() {
        reply.getCustomers().add(0,databaseManager.getCustomerDAOService().updateRole(request.getCustomers().get(0)));
    }

    private void getNotifications() {
        reply.setNotifications(databaseManager.getNotificationDAOService().readAll(request.getCustomers().get(0).getId(), request.getIndex()));
    }

    private void getNotification() {
        reply.getNotifications().add(0,databaseManager.getNotificationDAOService().read(request.getNotifications().get(0).getId(), request.getCustomers().get(0).getId()));
    }

    private void getAdmins() {
        reply.setCustomers(databaseManager.getCustomerDAOService().readAdmins());
    }

    private void sendNotification() {
        reply.getNotifications().add(0,databaseManager.getNotificationDAOService()
            .create(request.getCustomers().get(0).getId(), request.getNotifications().get(0).getText(), request.getNotifications().get(0).getTime(), request.getNotifications().get(0).getStatus()));
    }

    private void updateSeenNotification() {
        reply.getNotifications().add(0, databaseManager.getNotificationDAOService().update(request.getCustomers().get(0).getId(), request.getNotifications().get(0)));
    }

    private void getCustomerWithWishlistItem() {
        reply.setCustomers(databaseManager.getCustomerDAOService().customerWithWishlistItem(request.getItemId()));
    }
}
