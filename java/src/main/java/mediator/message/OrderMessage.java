package mediator.message;

import model.Order;

import java.util.ArrayList;
import java.util.List;

public class OrderMessage extends Message {
    private int index;
    private List<Order> orders;
    private int customerId;
    private String status;

    public OrderMessage(String service, String type) {
        super(service, type);
        orders = new ArrayList<>();
    }

    public int getIndex() {
        return index;
    }

    public void setIndex(int index) {
        this.index = index;
    }

    public List<Order> getOrders() {
        return orders;
    }

    public void setOrders(List<Order> orders) {
        this.orders = orders;
    }

    public int getCustomerId() {
        return customerId;
    }

    public void setCustomerId(int customerId) {
        this.customerId = customerId;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }
}
