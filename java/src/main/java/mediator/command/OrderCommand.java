package mediator.command;

import database.daomodel.DatabaseManager;
import mediator.message.OrderMessage;
import mediator.message.Message;
import model.Order;

import java.util.HashMap;

public class OrderCommand implements Command {
    private OrderMessage request;
    private OrderMessage reply;
    private DatabaseManager databaseManager;
    private HashMap<String, Runnable> methods;

    public OrderCommand(DatabaseManager databaseManager) {
        this.databaseManager = databaseManager;
        methods = new HashMap<>();
        methods.put("purchase",this::purchase);
        methods.put("getAll", this::getAll);
        methods.put("get", this::get);
        methods.put("update",this::update);
        methods.put("getAllByCustomer",this::getAllOrdersByCustomer);
        methods.put("returnItems", this::returnItems);
    }

    @Override public Message execute(Message request) {
        try {
            this.request = (OrderMessage) request;
            reply = new OrderMessage(request.getService(), request.getType());
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
            .create(order.getItems(), order.getAddress(), order.getDateTime(), order.getOrderStatus(), order.getFirstName(), order.getLastName(), order.getEmail(), order.getCustomerId()));
    }

    private void getAll() {
        reply.setOrders(databaseManager.getOrderDAOService().readByIndex(request.getIndex(), request.getCustomerId(), request.getStatus()));
    }

    private void get() {
        reply.setOrder(databaseManager.getOrderDAOService().read(request.getOrder().getId()));
    }

    private void update(){
        reply.setOrder(databaseManager.getOrderDAOService().update(request.getOrder()));
    }
    
    private void getAllOrdersByCustomer(){
        reply.setOrders(databaseManager.getOrderDAOService().readAllOrdersByCustomer(request.getCustomerId(), request.getIndex()));
    }

    private void returnItems() {
        databaseManager.getOrderDAOService().returnItems(request.getOrder());
    }
}
