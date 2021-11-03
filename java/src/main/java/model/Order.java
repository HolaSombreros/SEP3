package model;

import model.enums.OrderStatus;

import java.util.ArrayList;
import java.util.List;

public class Order {
    private List<Item> items;
    private String firstName;
    private String lastName;
    private String email;
    private int id;
    private Address address;
    private MyDateTime dateTime;
    private OrderStatus orderStatus;

    public Order() {
        items = new ArrayList<>();
    }

    public Order(List<Item> items, String firstName, String lastName, String email, int id, Address address, MyDateTime dateTime, OrderStatus orderStatus) {
        this.items = items;
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.id = id;
        this.address = address;
        this.dateTime = dateTime;
        this.orderStatus = orderStatus;
    }

    public void setItems(List<Item> items){
        this.items = items;
    }
    public List<Item> getItems() {
        return items;
    }

    public String getEmail() {
        return email;
    }

    public String getFirstName() {
        return firstName;
    }

    public String getLastName() {
        return lastName;
    }

    public int getId() {
        return id;
    }

    public Address getAddress() {
        return address;
    }

    public MyDateTime getDateTime() {
        return dateTime;
    }

    public OrderStatus getOrderStatus() {
        return orderStatus;
    }
}
