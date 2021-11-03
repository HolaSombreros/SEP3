package database.daoservice;

import database.daomodel.OrderDAO;
import database.daoservice.mapper.OrderMapper;
import model.*;
import model.enums.OrderStatus;

import java.sql.SQLException;
import java.util.List;

public class OrderDAOService implements OrderDAO {

    private DatabaseHelper<Order> databaseHelper;
    private AddressDAOService addressDAOService;
    private ItemDAOService itemDAOService;

    public OrderDAOService(String url, String username, String password) {
        this.databaseHelper = new DatabaseHelper<>(url, username, password);
        addressDAOService = new AddressDAOService(url, username,password);
        itemDAOService = new ItemDAOService(url, username, password);
    }

    @Override
    public Order create(List<Item> items, Address address, MyDateTime dateTime, OrderStatus status, String firstName, String lastName, String email) {
        try {
           // if(addressDAOService.read(address.getId()) != null)
            Address address1 = addressDAOService.create(address.getStreet(), address.getNumber(), address.getZipCode(), address.getCity());
            List<Integer> keys = databaseHelper.executeUpdateWithKeys("INSERT INTO purchase (address_id, date_time, status, first_name, last_name, email, customer_id) " +
                            "VALUES (?,?,?::order_status,?,?,?,?)", address1.getId(),dateTime.getLocalDateTime(),status.toString(), firstName, lastName, email, null);

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
        try {
            Order order =  databaseHelper.mapObject(new OrderMapper(), "SELECT * FROM purchase JOIN (SELECT * from address JOIN city USING (zip_code))a USING (address_id) WHERE order_id = ?", id);
            order.setItems(itemDAOService.readAllFromOrder(id));
            return order;
        }catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public void update(Order order) {

    }

    @Override
    public void delete(Order order) {

    }

    @Override
    public List<Order> readAll() {
        return null;
    }
}
