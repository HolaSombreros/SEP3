package model;

import model.enums.OrderStatus;
import java.util.List;

public class Order {
    private List<Item> items;
    private String firstName;
    private String lastName;
    private String email;
    private int id;
    private int customerId;
    private Address address;
    private MyDateTime dateTime;
    private OrderStatus orderStatus;


    public Order(List<Item> items, String firstName, String lastName, String email, int id, Address address, MyDateTime dateTime, OrderStatus orderStatus, int customerId) {
        this.items = items;
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.id = id;
        this.address = address;
        this.dateTime = dateTime;
        this.orderStatus = orderStatus;
        this.customerId = customerId;
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

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public void setId(int id) {
        this.id = id;
    }

    public int getCustomerId() {
        return customerId;
    }

    public void setCustomerId(int customerId) {
        this.customerId = customerId;
    }

    public void setAddress(Address address) {
        this.address = address;
    }

    public void setDateTime(MyDateTime dateTime) {
        this.dateTime = dateTime;
    }

    public void setOrderStatus(OrderStatus orderStatus) {
        this.orderStatus = orderStatus;
    }
}
