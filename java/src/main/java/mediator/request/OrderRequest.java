package mediator.request;

import model.Order;

import java.util.List;

public class OrderRequest extends Request{
    private int index;
    private Order order;
    private List<Order> orders;

    public OrderRequest(String service, String type) {
        super(service, type);
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

    public Order getOrder() {
        return order;
    }

    public void setOrder(Order order) {
        this.order = order;
    }
}
