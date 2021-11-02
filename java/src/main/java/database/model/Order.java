package database.model;

import database.model.enums.OrderStatus;

import java.util.ArrayList;
import java.util.List;

public class Order {
    private List<Item> items;
    private User user;
    private int id;
    private Address address;
    private MyDateTime dateTime;
    private OrderStatus orderStatus;

    public Order() {
        items = new ArrayList<>();
    }

    public Order(List<Item> items, User user, int id, Address address, MyDateTime dateTime, OrderStatus orderStatus) {
        this.items = items;
        this.user = user;
        this.id = id;
        this.address = address;
        this.dateTime = dateTime;
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

    public MyDateTime getDatetime() {
        return dateTime;
    }

    public OrderStatus getOrderStatus() {
        return orderStatus;
    }
}
