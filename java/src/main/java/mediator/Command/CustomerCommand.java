package mediator.Command;

import database.daomodel.DatabaseManager;
import mediator.Request.CustomerRequest;
import mediator.Request.ErrorRequest;
import mediator.Request.Request;
import model.Customer;

public class CustomerCommand implements Command {

    private DatabaseManager databaseManager;

    public CustomerCommand(DatabaseManager databaseManager) {
        this.databaseManager = databaseManager;
    }

    @Override public Request execute(Request request) {
        CustomerRequest reply = new CustomerRequest(request.getService(), request.getType());
        switch (request.getType()) {
            case "login":
                reply.setCustomer(databaseManager.getCustomerDAOService().read(((CustomerRequest) request).getCustomer().getEmail()));
                if (!((CustomerRequest)request).getCustomer().getPassword().equals(reply.getCustomer().getPassword())) {
                    ErrorRequest error = new ErrorRequest("error", "error");
                    error.setMessage("Incorrect password");
                    return error;
                }
                return reply;
            case "register":
                Customer customer = ((CustomerRequest)request).getCustomer();
                reply.setCustomer(databaseManager.getCustomerDAOService().create(customer.getFirstName(), customer.getLastName(), customer.getEmail(), customer.getPassword(),
                    customer.getRole(), customer.getAddress(), customer.getPhoneNumber()));
                return reply;
        }
        throw new IllegalArgumentException("The request could not be fulfilled");
    }
}
