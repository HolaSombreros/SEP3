package database.daoservice;

import database.daomodel.OrderDAO;
import database.daoservice.mapper.OrderMapper;
import database.model.*;
import database.model.enums.ItemStatus;
import database.model.enums.OrderStatus;

import java.sql.SQLException;
import java.time.LocalTime;
import java.time.LocalDateTime;
import java.util.Collection;
import java.time.LocalDate;
import java.util.List;

public class OrderDAOService implements OrderDAO {

    private DatabaseHelper<Order> databaseHelper;

    public OrderDAOService(String url, String username, String password) {
        this.databaseHelper = new DatabaseHelper<>(url, username, password);
    }

    @Override
    public Order create(List<Item> items, Address address, MyDateTime dateTime, OrderStatus status, User user) {
        try {
            List<Integer> keys = databaseHelper.executeUpdateWithKeys("INSERT INTO purchase (address_id, date_time, status, first_name, middle_name, last_name, email, customer_id) " +
                            "VALUES (?,?,?,?,?,?,?,?", address.getId(),dateTime.getLocalDateTime(),status.toString(), user.getFirstName(), user.getMiddleName(),
                    user.getLastName(), user.getEmail(), null);

            for(Item item: items){
                databaseHelper.executeUpdate("INSERT INTO order_item (order_id, item_id, item_quantity, item_price) VALUES (?,?,?,?)",keys.get(0) ,item.getId(), item.getQuantity(), item.getPrice());
            }
            return read(keys.get(0));

        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public Order read(int id) {
        return null;
    }

    @Override
    public void update(Order order) {

    }

    @Override
    public void delete(Order order) {

    }

    @Override
    public Collection<Order> readAll() {
        return null;
    }
}
