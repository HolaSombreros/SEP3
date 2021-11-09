package mediator.Request;

import model.Order;

public class OrderRequest extends Request{
    private Order order;

    public OrderRequest(String service, String type) {
        super(service, type);
    }

    public Order getOrder() {
        return order;
    }

    public void setOrder(Order order) {
        this.order = order;
    }
}
