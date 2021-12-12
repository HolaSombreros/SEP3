package database.daoservice;

import database.daomodel.CategoryDAO;
import database.daomodel.ItemDAO;
import database.daoservice.mapper.ItemMapper;
import model.Category;
import model.Item;
import model.enums.ItemStatus;
import java.math.BigDecimal;
import java.sql.SQLException;
import java.util.List;

public class ItemDAOService implements ItemDAO {

    private DatabaseHelper<Item> databaseHelper;
    private CategoryDAO categoryDaoService;

    public ItemDAOService(String url, String username, String password) {
        databaseHelper = new DatabaseHelper<>(url, username, password);
        categoryDaoService = new CategoryDAOService(url, username, password);

    }

    @Override
    public Item create(String name, String description, BigDecimal price, Category category, int quantity, String imgFilepath){
        try {
            List<Integer> categoryKeys = null;
            List<Integer> keys= null;
            Category existing = categoryDaoService.read(category.getName());
            if(existing== null) {
                categoryKeys = databaseHelper.executeUpdateWithKeys("INSERT INTO category (name) VALUES (?)", category.getName());
                keys = databaseHelper.executeUpdateWithKeys("INSERT INTO item (name, description, price, category_id, discount, quantity, status, image_filepath) VALUES (?,?,?,?,?,?,?::item_status,?)",
                        name, description,price,categoryKeys.get(0),0,quantity, ItemStatus.INSTOCK.toString(), imgFilepath);

            }
            else{
               keys = databaseHelper.executeUpdateWithKeys("INSERT INTO item (name, description, price, category_id, discount, quantity, status, image_filepath) VALUES (?,?,?,?,?,?,?::item_status,?)",
                        name, description,price,existing.getId(),0,quantity, ItemStatus.INSTOCK.toString(), imgFilepath);
            }
            return read(keys.get(0));

        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public Item read(int id) {
        try {
            return databaseHelper.mapObject(new ItemMapper(), "SELECT *, category.name as category_name, item.name AS item_name FROM item JOIN category USING (category_id) WHERE item_id = ?;", id);
        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public Item read(String name, String description, Category category) {
        try {
            return databaseHelper.mapObject(new ItemMapper(), "SELECT *, c.name AS category_name, i.name AS item_name FROM item i JOIN category c USING (category_id) WHERE i.name = ? AND description = ? AND c.name = ?", name, description, category.getName());
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public Item update(Item item) {
        try {
            if(categoryDaoService.read(item.getCategory().getName()) == null) {
                List<Integer> key =databaseHelper.executeUpdateWithKeys("INSERT INTO category (name) VALUES (?)", item.getCategory().getName());
                item.getCategory().setId(key.get(0));
            }
            databaseHelper.executeUpdate("UPDATE item SET name = ?, description = ?, price = ?, category_id = ?, quantity = ?, status = ?::item_status, discount =?, image_filepath = ? WHERE item_id = ?", item.getName(), item.getDescription(),
                    item.getPrice(), item.getCategory().getId(), item.getQuantity(), item.getStatus().toString(), item.getDiscount(), item.getImageName(), item.getId());
            return item;
        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }


    @Override
    public List<Item> readByIndex(int index, String category, String priceOrder, String ratingOrder, String discountOrder, String statusOrder, String search) {
        try{
            if(search != null){
                return databaseHelper.mapList(new ItemMapper(),"SELECT *, category.name AS category_name, item.name AS item_name FROM item JOIN category USING(category_id) WHERE lower(item.name) ~ lower(?) ORDER BY item_id DESC LIMIT 21 OFFSET 21 * ?",search, index);
            }
            if(category != null){
               return databaseHelper.mapList(new ItemMapper(),"SELECT *, category.name AS category_name, item.name AS item_name FROM item JOIN category USING(category_id) WHERE category.name = ? ORDER BY item_id DESC LIMIT 21 OFFSET 21 * ?", category, index);
            }
            if(priceOrder != null){
                if(priceOrder.equalsIgnoreCase("ascending"))
                    return databaseHelper.mapList(new ItemMapper(),"SELECT *, category.name AS category_name, item.name AS item_name FROM item JOIN category USING(category_id) ORDER BY (price - item.price*discount/100) LIMIT 21 OFFSET 21 * ?", index);
                else
                    return databaseHelper.mapList(new ItemMapper(),"SELECT *, category.name AS category_name, item.name AS item_name FROM item JOIN category USING(category_id) ORDER BY (price - item.price*discount/100) DESC LIMIT 21 OFFSET 21 * ?", index);
            }
            if(ratingOrder != null){
                if(ratingOrder.equalsIgnoreCase("ascending")){
                    return databaseHelper.mapList(new ItemMapper(),"SELECT description, price, discount, quantity, status,item_id, image_filepath,category_id,category.name AS category_name, item.name AS item_name,COALESCE(AVG(rating),0) AS avg FROM item JOIN category USING(category_id) Left JOIN review USING (item_id)\n" +
                            "group by category.name, item.name, description, price, discount, quantity, status, image_filepath,item_id,category_id\n" +
                            "ORDER BY avg  LIMIT 21 OFFSET 21 * ?", index);
                }
                else{
                    return databaseHelper.mapList(new ItemMapper(),"SELECT description, price, discount, quantity, status,item_id,category_id, image_filepath,category.name AS category_name, item.name AS item_name,COALESCE(AVG(rating),0) AS avg FROM item JOIN category USING(category_id) Left JOIN review USING (item_id)\n" +
                            "group by category.name, item.name, description, price, discount, quantity, status, image_filepath,item_id,category_id\n" +
                            "ORDER BY avg DESC LIMIT 21 OFFSET 21 * ?", index);

                }
            }

            if (discountOrder != null) {
                if (discountOrder.equalsIgnoreCase("ASC")) {
                    return databaseHelper.mapList(new ItemMapper(),"SELECT *, category.name AS category_name, item.name AS item_name FROM item JOIN category USING(category_id) ORDER BY discount ASC LIMIT 21 OFFSET 21 * ?", index);
                } else {
                    return databaseHelper.mapList(new ItemMapper(),"SELECT *, category.name AS category_name, item.name AS item_name FROM item JOIN category USING(category_id) ORDER BY discount DESC LIMIT 21 OFFSET 21 * ?", index);
                }
            }

            if (statusOrder != null) {
                if (statusOrder.equalsIgnoreCase("In Stock")) {
                    return databaseHelper.mapList(new ItemMapper(),"SELECT *, category.name AS category_name, item.name AS item_name FROM item JOIN category USING(category_id) WHERE status = 'In Stock' LIMIT 21 OFFSET 21 * ?", index);
                } else {
                    return databaseHelper.mapList(new ItemMapper(),"SELECT *, category.name AS category_name, item.name AS item_name FROM item JOIN category USING(category_id) WHERE status = 'Out of Stock' LIMIT 21 OFFSET 21 * ?", index);
                }
            }

            return databaseHelper.mapList(new ItemMapper(), "SELECT *, category.name as category_name, item.name AS item_name FROM item JOIN category USING (category_id) ORDER BY item_id DESC LIMIT 21 OFFSET 21 * ?",index);
        }catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public List<Item> readAllFromOrder(int orderId) {
        try {
            return databaseHelper.mapList(new ItemMapper(), "SELECT item_id, item.name AS item_name, description, category_id, discount, status, purchase_id, purchase_item.quantity, purchase_item.price, image_filepath, category.name AS category_name FROM item JOIN category USING (category_id) JOIN purchase_item USING (item_id) WHERE purchase_id = ?;",orderId);
        }catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public List<Item> readAllByIds(int[] itemIds) {
        try {
            String query = "SELECT *, category.name AS category_name, item.name AS item_name FROM item JOIN category USING (category_id) WHERE item_id IN (";
            for (int i = 0; i < itemIds.length; i++) {
                if (i == 0) {
                    query += "?";
                }
                else {
                    query += ", ?";
                }
            }
            query += ") ORDER BY item_id ASC;";

            Object[] ids = new Object[itemIds.length];
            for (int i = 0; i < ids.length; i++) {
                ids[i] = itemIds[i];
            }

            return databaseHelper.mapList(new ItemMapper(), query, ids);
        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }
    @Override public List<Item> readCustomerWishlist(int customerId) {
        try {
            return databaseHelper.mapList(new ItemMapper(), "SELECT item_id, item.name AS item_name, description, price, category_id, discount, quantity, status, image_filepath, category.name AS category_name "
                    + "FROM item JOIN wishlist_item USING (item_id) "
                    + "JOIN customer USING (customer_id) "
                    + "JOIN category USING (category_id) "
                    + "WHERE customer_id = ?;", customerId);
        } catch (SQLException e) {
            throw new IllegalStateException(e.getMessage());
        }
    }
    
    @Override public void addWishlist(int customerId, int itemId) {
        try {
            databaseHelper.executeUpdate("INSERT INTO wishlist_item(customer_id, item_id) VALUES (?,?);", customerId, itemId);
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override public void removeItemFromWishlist(int customerId, int itemId) {
        try {
            databaseHelper.executeUpdate("DELETE FROM wishlist_item WHERE customer_id = ? AND item_id = ?;", customerId, itemId);
        } catch (SQLException e) {
            throw new IllegalStateException(e.getMessage());
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
            return databaseHelper.mapList(new ItemMapper(),"SELECT *, category.name AS category_name, item.name AS item_name FROM shopping_cart_item JOIN item USING(item_id) JOIN category USING(category_id) WHERE customer_id = ?;", customerId);
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
            databaseHelper.executeUpdate("DELETE FROM shopping_cart_item WHERE item_id = ? AND customer_id = ?;", item.getId(), customerId);
        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }
}
