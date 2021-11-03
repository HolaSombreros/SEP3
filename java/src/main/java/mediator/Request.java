package mediator;

import model.Item;
import model.Order;

import java.util.ArrayList;
import java.util.List;

public class Request {
    private String type;
    private List<Item> items;
    private Order order;

    public Request(String type) {
        this.type = type;
        items = new ArrayList<>();
        order = new Order();
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public List<Item> getItems() {
        return items;
    }

    public void setItems(List<Item> items) {
        this.items = items;
    }

    public Order getOrder() {
        return order;
    }

    public void setOrder(Order order) {
        this.order = order;
    }
}
