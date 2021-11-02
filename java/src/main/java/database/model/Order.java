package database.model;

import database.model.enums.OrderStatus;

import java.time.LocalDateTime;
import java.util.List;

public class Order {
    private List<Item> items;
    private User user;
    private int id;
    private Address address;
    private LocalDateTime datetime;
    private OrderStatus orderStatus;

    public Order(List<Item> items, User user, int id, Address address, LocalDateTime datetime, OrderStatus orderStatus) {
        this.items = items;
        this.user = user;
        this.id = id;
        this.address = address;
        this.datetime = datetime;
        this.orderStatus = orderStatus;
    }

    public List<Item> getItems() {
        return items;
    }

    public User getUser() {
        return user;
    }

    public int getId() {
        return id;
    }

    public Address getAddress() {
        return address;
    }

    public LocalDateTime getDatetime() {
        return datetime;
    }

    public OrderStatus getOrderStatus() {
        return orderStatus;
    }
}
