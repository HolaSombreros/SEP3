package mediator.Command;

import database.daomodel.DatabaseManager;
import mediator.Request.OrderRequest;
import mediator.Request.Request;
import model.Order;

public class OrderCommand implements Command {

    private DatabaseManager databaseManager;

    public OrderCommand(DatabaseManager databaseManager) {
        this.databaseManager = databaseManager;
    }

    @Override public Request execute(Request request) {
        OrderRequest reply = new OrderRequest(request.getService(), request.getType());;
        switch (request.getType()) {
            case "purchase":
                Order order = ((OrderRequest) request).getOrder();
                reply.setOrder(databaseManager.getOrderDAOService()
                    .create(order.getItems(), order.getAddress(), order.getDateTime(), order.getOrderStatus(), order.getFirstName(), order.getLastName(), order.getEmail()));
                return reply;
        }
        throw new IllegalArgumentException("The request could not be fulfilled");
    }
}
