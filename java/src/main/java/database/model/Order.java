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
}
