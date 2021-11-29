package mediator.Command;

import database.daomodel.DatabaseManager;
import mediator.Request.ItemRequest;
import mediator.Request.OrderRequest;
import mediator.Request.Request;
import model.Order;

import java.util.HashMap;

public class OrderCommand implements Command {

    private OrderRequest request;
    private OrderRequest reply;
    private DatabaseManager databaseManager;
    private HashMap<String, Runnable> methods;

    public OrderCommand(DatabaseManager databaseManager) {
        this.databaseManager = databaseManager;
        methods = new HashMap<>();
        methods.put("purchase",this::purchase);
        methods.put("getAll", this::getAll);
    }

    @Override public Request execute(Request request) {
        try {
            this.request = (OrderRequest) request;
            reply = new OrderRequest(request.getService(), request.getType());
            methods.get(request.getType()).run();
            return reply;
        }
        catch (Exception e) {
             e.printStackTrace();
            throw new IllegalArgumentException("The request could not be fulfilled");
        }
    }

    private void purchase() {
        Order order = request.getOrder();
        reply.setOrder(databaseManager.getOrderDAOService()
            .create(order.getItems(), order.getAddress(), order.getDateTime(), order.getOrderStatus(), order.getFirstName(), order.getLastName(), order.getEmail()));
    }

    private void getAll() {
        reply.setOrders(databaseManager.getOrderDAOService().readByIndex(request.getIndex()));
    }
}
