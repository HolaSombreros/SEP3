package database.daoservice;

import database.daomodel.ItemDAO;
import database.daoservice.mapper.ItemMapper;
import database.model.Item;
import database.model.enums.Category;
import database.model.enums.ItemStatus;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.List;

public class ItemDAOService implements ItemDAO {

    private DatabaseHelper<Item> databaseHelper;

    public ItemDAOService(String url, String username, String password) {
        databaseHelper = new DatabaseHelper<>(url, username, password);
    }

    @Override
    public Item create(String name, String description, double price, Category category, int quantity){
        try {
            List<Integer> keys = databaseHelper.executeUpdateWithKeys("INSERT INTO item (name, description, price, category, discount, quantity, status) VALUES (?,?,?,?,?,?,?",
                    name, description,price,category.toString(),0,quantity, ItemStatus.INSTOCK);
            return read(keys.get(0));

        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public Item read(int id) {
        try {
            return databaseHelper.mapObject(new ItemMapper(), "SELECT * FROM item WHERE id = ?", id);
        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public void update(Item item) {
        try {
            databaseHelper.executeUpdate("UPDATE item SET name = ?, description = ?, price = ?, category = ?, quantity = ?, status = ?, discount =? WHERE item_id = ?", item.getName(), item.getDescription(),
                    item.getPrice(), item.getCategory().toString(), item.getQuantity(), item.getStatus().toString(), item.getDiscount(), item.getId());
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
}
