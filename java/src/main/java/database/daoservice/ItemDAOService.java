package database.daoservice;

import database.daomodel.ItemDAO;
import database.daoservice.mapper.ItemMapper;
import model.Item;
import model.enums.Category;
import model.enums.ItemStatus;

import java.sql.SQLException;
import java.util.List;

public class ItemDAOService implements ItemDAO {

    private DatabaseHelper<Item> databaseHelper;

    public ItemDAOService(String url, String username, String password) {
        databaseHelper = new DatabaseHelper<>(url, username, password);
    }

    @Override
    public Item create(String name, String description, double price, Category category, int quantity, String imgFilepath){
        try {
            List<Integer> keys = databaseHelper.executeUpdateWithKeys("INSERT INTO item (name, description, price, category, discount, quantity, status, image_filepath) VALUES (?,?,?,?::item_category,?,?,?::item_status,?)",
                    name, description,price,category.toString(),0,quantity, ItemStatus.INSTOCK.toString(), imgFilepath);
            return read(keys.get(0));

        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public Item read(int id) {
        try {
            return databaseHelper.mapObject(new ItemMapper(), "SELECT * FROM item WHERE item_id = ?;", id);
        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public void update(Item item) {
        try {
            databaseHelper.executeUpdate("UPDATE item SET name = ?, description = ?, price = ?, category = ?::item_category, quantity = ?, status = ?::item_status, discount =?, image_filepath = ? WHERE item_id = ?", item.getName(), item.getDescription(),
                    item.getPrice(), item.getCategory().toString(), item.getQuantity(), item.getStatus().toString(), item.getDiscount(), item.getImageName(), item.getId());
        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public void delete(Item item) {
        //TODO think about this
    }

    @Override
    public List<Item> readByCategory(Category category) {
        return null;
    }

    @Override
    public List<Item> readByIndex(int index) {
        try{
            return databaseHelper.mapList(new ItemMapper(), "SELECT * FROM item ORDER BY item_id DESC LIMIT 21 OFFSET 21 * ?",index);
        }catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public List<Item> readAllFromOrder(int orderId) {
        try{
            return databaseHelper.mapList(new ItemMapper(), "SELECT item_id,name,description,category,discount,status, purchase_id, order_item.quantity, purchase_item.price,image_filepath FROM item JOIN purchase_item USING (item_id) WHERE purchase_id = ?;",orderId);
        }catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override public List<Item> readCustomerWishlist(int customerId) {
        try {
            return databaseHelper.mapList(new ItemMapper(), "SELECT item_id, name, description, price, category, discount, quantity, status, image_filepath "
                    + "FROM item JOIN wishlist_item USING (item_id) "
                    + "JOIN customer USING (customer_id) "
                    + "WHERE customer_id = ?;", customerId);
        } catch (SQLException e) {
            throw new IllegalStateException(e.getMessage());
        }
    }

    @Override public void removeItemFromWishlist(int customerId, int itemId) {
        try {
            databaseHelper.executeUpdate("DELETE FROM wishlist_item WHERE customer_id = ? AND item_id = ?;", customerId, itemId);
        } catch (SQLException e) {
            throw new IllegalStateException(e.getMessage());
        }
    }
}
