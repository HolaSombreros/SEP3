package database.daoservice;

import database.daomodel.ItemDAO;
import database.daoservice.mapper.ItemMapper;
import model.Category;
import model.Item;
import model.enums.ItemStatus;

import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

public class ItemDAOService implements ItemDAO {

    private DatabaseHelper<Item> databaseHelper;

    public ItemDAOService(String url, String username, String password) {
        databaseHelper = new DatabaseHelper<>(url, username, password);
    }

    @Override
    public Item create(String name, String description, double price, Category category, int quantity, String imgFilepath){
        try {
            List<Integer> categoryKeys = null;
            List<Integer> keys= null;
            if(!isCategory(category.getName())) {
                categoryKeys = databaseHelper.executeUpdateWithKeys("INSERT INTO category (name) VALUES (?)", category.getName());
               keys = databaseHelper.executeUpdateWithKeys("INSERT INTO item (name, description, price, category_id, discount, quantity, status, image_filepath) VALUES (?,?,?,?,?,?,?::item_status,?)",
                        name, description,price,categoryKeys.get(0),0,quantity, ItemStatus.INSTOCK.toString(), imgFilepath);
            }
            else{
               keys = databaseHelper.executeUpdateWithKeys("INSERT INTO item (name, description, price, category_id, discount, quantity, status, image_filepath) VALUES (?,?,?,?,?,?,?::item_status,?)",
                        name, description,price,category.getId(),0,quantity, ItemStatus.INSTOCK.toString(), imgFilepath);
            }
            return read(keys.get(0));

        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public Item read(int id) {
        try {
            return databaseHelper.mapObject(new ItemMapper(), "SELECT * FROM item JOIN category USING (category_id) WHERE item_id = ?;", id);
        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public void update(Item item) {
        try {
            databaseHelper.executeUpdate("UPDATE item SET name = ?, description = ?, price = ?, category_id = ?, quantity = ?, status = ?::item_status, discount =?, image_filepath = ? WHERE item_id = ?", item.getName(), item.getDescription(),
                    item.getPrice(), item.getCategory().getId(), item.getQuantity(), item.getStatus().toString(), item.getDiscount(), item.getImageName(), item.getId());
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
            return databaseHelper.mapList(new ItemMapper(), "SELECT * FROM item JOIN category USING (category_id) ORDER BY item_id DESC LIMIT 21 OFFSET 21 * ?",index);
        }catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public List<Item> readAllFromOrder(int orderId) {
        try {
            return databaseHelper.mapList(new ItemMapper(), "SELECT item_id, name, description, category_id, discount, status, purchase_id, purchase_item.quantity, purchase_item.price, image_filepath FROM item JOIN category USING (category_id) JOIN purchase_item USING (item_id) WHERE purchase_id = ?;",orderId);
        }catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    /*
    //TODO: convert int[] to optional parameter, ask OLE
    String query = "SELECT * FROM item WHERE ";
            for(int i =0; i < itemIds.length; i++) {
                if(i ==0)
                    query += "item_id=?";
                else
                    query += " OR item_id=?";
            }
     */
    @Override
    public List<Item> readAllByIds(int[] itemIds) {
        try {
            List<Item> items = new ArrayList<>();
            for(int i = 0; i < itemIds.length; i++) {
               items.add(databaseHelper.mapObject(new ItemMapper(), "SELECT * FROM item JOIN category USING (category_id) WHERE item_id=?", itemIds[i]));
            }
            return items;
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }
    @Override public List<Item> readCustomerWishlist(int customerId) {
        try {
            return databaseHelper.mapList(new ItemMapper(), "SELECT item_id, name, description, price, category_id, discount, quantity, status, image_filepath "
                    + "FROM item JOIN wishlist_item USING (item_id) "
                    + "JOIN customer USING (customer_id) "
                    + "JOIN category USING (category_id) "
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

    @Override
    public List<Item> readByItemName(String itemName, int index) {
        try{
            return databaseHelper.mapList(new ItemMapper(),"SELECT * FROM item WHERE lower(name) ~ lower(?) ORDER BY item_id DESC LIMIT 21 OFFSET 21 * ?",itemName, index);
        }
        catch (SQLException e){
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override public void addToShoppingCart(Item item, int customerId) {
        try {
            databaseHelper.executeUpdate("INSERT INTO shopping_cart_item(customer_id, item_id, quantity) VALUES (?,?,?);", customerId, item.getId(), item.getQuantity());
        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override public List<Item> readShoppingCart(int customerId) {
        try {
            return databaseHelper.mapList(new ItemMapper(),"SELECT * FROM shopping_cart_item JOIN item USING(item_id) WHERE customer_id = ?;", customerId);
        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override public void updateShoppingCart(Item item, int customerId) {
        try {
            databaseHelper.executeUpdate("UPDATE shopping_cart_item SET quantity = ? WHERE customer_id = ? AND item_id = ?;", item.getQuantity(), customerId, item.getId());
        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override public void removeFromShoppingCart(Item item, int customerId) {
        try {
            databaseHelper.executeUpdate("DELETE FROM shopping_cart_item WHERE item_id = ? AND customer_id = ?;", item.getQuantity(), customerId, item.getId());
        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    private boolean isCategory(String category) {
        try{
            return databaseHelper.executeQuery(databaseHelper.getConnection(), "SELECT * FROM category WHERE name = ?", category).next();
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }
}
