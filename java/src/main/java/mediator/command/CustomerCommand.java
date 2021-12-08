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
        methods.put("get", this::get);
        methods.put("login", this::login);
        methods.put("register", this::register);
        methods.put("update", this::update);
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
        reply.setCustomer(databaseManager.getCustomerDAOService()
            .create(customer.getFirstName(), customer.getLastName(), customer.getEmail(), customer.getPassword(), customer.getRole(), customer.getAddress(),
                customer.getPhoneNumber()));
    }

    private void update() {
        reply.setCustomer(databaseManager.getCustomerDAOService().update(request.getCustomer()));
    }

    private void getCustomersByIndex() {
        reply.setCustomers(databaseManager.getCustomerDAOService().readByIndex(request.getIndex()));
    }

    private void updateRole() {
        reply.setCustomer(databaseManager.getCustomerDAOService().updateRole(request.getCustomer()));
    }

    private void getNotifications() {
        reply.setNotifications(databaseManager.getNotificationDAOService().readAll(request.getCustomer().getId(), request.getIndex()));
    }

    private void getNotification() {
        reply.setNotification(databaseManager.getNotificationDAOService().read(request.getNotification().getId(), request.getCustomer().getId()));
    }

    private void getAdmins() {
        reply.setCustomers(databaseManager.getCustomerDAOService().readAdmins());
    }

    private void sendNotification() {
        reply.setNotification(databaseManager.getNotificationDAOService()
            .create(request.getCustomer().getId(), request.getNotification().getText(), request.getNotification().getTime(), request.getNotification().getStatus()));
    }

    private void updateSeenNotification() {
        reply.setNotification(databaseManager.getNotificationDAOService().update(request.getCustomer().getId(), request.getNotification()));
    }

    private void getCustomerWithWishlistItem() {
        reply.setCustomers(databaseManager.getCustomerDAOService().customerWithWishlistItem(request.getItemId()));
    }
}
