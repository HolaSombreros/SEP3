package database.daoservice;

import database.daomodel.ItemDAO;
import database.daomodel.OrderDAO;
import database.daoservice.mapper.OrderMapper;
import model.*;
import model.enums.ItemStatus;
import model.enums.OrderStatus;

import java.math.BigDecimal;
import java.sql.SQLException;
import java.util.List;

public class OrderDAOService implements OrderDAO {
    private DatabaseHelper<Order> databaseHelper;
    private AddressDAOService addressDAOService;
    private ItemDAO itemDAOService;

    public OrderDAOService(String url, String username, String password) {
        this.databaseHelper = new DatabaseHelper<>(url, username, password);
        addressDAOService = new AddressDAOService(url, username, password);
        itemDAOService = new ItemDAOService(url, username, password);
    }

    @Override public Order create(List<Item> items, Address address, MyDateTime dateTime, OrderStatus status, String firstName, String lastName, String email, int customerId) {
        try {
            Address address1 = addressDAOService.create(address.getStreet(), address.getNumber(), address.getZipCode(), address.getCity());
            List<Integer> keys = databaseHelper.executeUpdateWithKeys(
                "INSERT INTO purchase (address_id, date_time, status, first_name, last_name, email, customer_id) " + "VALUES (?,?,?::purchase_status,?,?,?,?);", address1.getId(),
                dateTime.getLocalDateTime(), status.toString(), firstName, lastName, email, customerId == 0 ? null : customerId);

            for (Item item : items) {
                BigDecimal discount = BigDecimal.valueOf((double) (100 - item.getDiscount()) / 100);
                databaseHelper.executeUpdate("INSERT INTO purchase_item (purchase_id, item_id, quantity, price) VALUES (?,?,?,?);", keys.get(0), item.getId(), item.getQuantity(),
                        item.getPrice().multiply(discount));

                Item item1 = itemDAOService.read(item.getId());
                item1.setQuantity(item1.getQuantity() - item.getQuantity());
                if (item1.getQuantity() == 0)
                    item1.setStatus(ItemStatus.OUTOFSTOCK);
                itemDAOService.update(item1);
            }
            return read(keys.get(0));

        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override public Order read(int id) {
        try {
            Order order = databaseHelper.mapObject(new OrderMapper(),
                "SELECT * FROM purchase JOIN (SELECT * from address JOIN city USING (zip_code))a USING (address_id) WHERE purchase_id = ?;", id);
            order.setItems(itemDAOService.readAllFromOrder(id));
            return order;
        }
        catch (SQLException e) {
            e.printStackTrace();
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override public List<Order> readByIndex(int index, int id, String status) {
        try {
            List<Order> orders;
            if (id == 0 && status.equals("Any"))
                orders = databaseHelper.mapList(new OrderMapper(),
                    "SELECT * FROM purchase JOIN (SELECT * from address JOIN city USING (zip_code))a USING (address_id) ORDER BY purchase_id DESC LIMIT 5 OFFSET 5 * ?", index);
            else if (id == 0)
                orders = databaseHelper.mapList(new OrderMapper(),
                    "SELECT * FROM purchase JOIN (SELECT * from address JOIN city USING (zip_code))a USING (address_id) WHERE status = ?::purchase_status ORDER BY purchase_id DESC LIMIT 5 OFFSET 5 * ?",
                    status, index);
            else if (status.equals("Any"))
                orders = databaseHelper.mapList(new OrderMapper(),
                    "SELECT * FROM purchase JOIN (SELECT * from address JOIN city USING (zip_code))a USING (address_id) WHERE customer_id = ? ORDER BY purchase_id DESC LIMIT 5 OFFSET 5 * ?",
                    id, index);
            else
                orders = databaseHelper.mapList(new OrderMapper(),
                    "SELECT * FROM purchase JOIN (SELECT * from address JOIN city USING (zip_code))a USING (address_id) WHERE customer_id = ? AND status = ?::purchase_status ORDER BY purchase_id DESC LIMIT 5 OFFSET 5 * ?",
                    id, status, index);
            for (Order order : orders)
                order.setItems(itemDAOService.readAllFromOrder(order.getId()));
            return orders;
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override public Order update(Order order) {
        try {
            Address address = addressDAOService.create(order.getAddress().getStreet(), order.getAddress().getNumber(), order.getAddress().getZipCode(),
                order.getAddress().getCity());
            databaseHelper.executeUpdate("UPDATE purchase SET address_id = ?, first_name = ?, last_name = ?, email = ?, status = ?::purchase_status WHERE purchase_id = ? AND customer_id = ?;", address.getId(),
                order.getFirstName(), order.getLastName(), order.getEmail(), order.getOrderStatus().toString(), order.getId(), order.getCustomerId());
            return read(order.getId());
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override public List<Order> readAllOrdersByCustomer(int customerId, int index) {
        try {
            List<Order> orders = databaseHelper.mapList(new OrderMapper(),
                "SELECT * FROM purchase JOIN (SELECT * from address JOIN city USING (zip_code))a USING (address_id) WHERE customer_id = ? ORDER BY purchase_id DESC LIMIT 21 OFFSET 21 * ?",
                customerId, index);
            for (Order order : orders) {
                order.setItems(itemDAOService.readAllFromOrder(order.getId()));
            }
            return orders;
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override public void returnItems(Order order) {
        try {
            for (int i = 0; i < order.getItems().size(); i++) {
                Item item = order.getItems().get(i);

                if (item.getQuantity() > 0) {
                    databaseHelper.executeUpdate("UPDATE purchase_item SET quantity = quantity - ? WHERE purchase_id = ? AND item_id = ?;", item.getQuantity(), order.getId(), item.getId());
                    databaseHelper.executeUpdate("UPDATE item SET quantity = quantity + ?, status = ?::item_status WHERE item_id = ?;", item.getQuantity(), ItemStatus.INSTOCK.toString(), item.getId());
                }
            }
        } catch (SQLException e) {
            throw new IllegalStateException(e.getMessage());
        }
    }
}
