package mediator;

import database.model.Item;
import database.model.Order;

import java.util.ArrayList;
import java.util.List;

public class Request {
    private String type;
    private List<Item> items;
    private Order order;
    private Item item;

    public Request(String type) {
        this.type = type;
        items = new ArrayList<>();
        order = new Order();
        item = new Item();
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public Item getItem() {
        return item;
    }

    public void setItem(Item item){
        this.item = item;
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
