package database.daomodel;

import database.model.*;
import database.model.enums.OrderStatus;

import java.time.LocalDateTime;
import java.util.Collection;
import java.util.List;

public interface OrderDAO {
    Order create(List<Item> items, Address address, MyDateTime dateTime, OrderStatus status, User user);
    Order read(int id);
    void update(Order order);
    void delete(Order order);
    Collection<Order> readAll();
}
