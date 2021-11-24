package database.daomodel;

import model.Item;
import model.enums.Category;

import java.util.List;

public interface ItemDAO {
    //CRUD metods
    Item create(String name, String description, double price, Category category, int quantity, String imgFilepath);
    Item read(int id);
    void update(Item item);
    void delete(Item item);
    List<Item> readByCategory(Category category);
    List<Item> readByIndex(int index);
    List<Item> readAllFromOrder(int orderId);
    List<Item> readAllByIds(int[] itemIds);
    List<Item> readCustomerWishlist(int customerId);
    void removeItemFromWishlist(int customerId, int itemId);
    void addToShoppingCart(Item item, int customerId);
    List<Item> readShoppingCart (int customerId);
    void updateShoppingCart (Item item, int customerId);
    void removeFromShoppingCart (Item item, int customerId);
    List<Item> readByItemName(String itemName, int index);
}
