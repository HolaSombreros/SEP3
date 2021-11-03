package database.daomodel;

import model.*;
import model.enums.OrderStatus;
import model.MyDateTime;

import java.util.Collection;
import java.util.List;

public interface OrderDAO {
    Order create(List<Item> items, Address address, MyDateTime dateTime, OrderStatus status, String firstName, String lastName, String email);
    Order read(int id);
    void update(Order order);
    void delete(Order order);
    List<Order> readAll();
}
