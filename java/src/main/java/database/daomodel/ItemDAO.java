package database.daomodel;

import model.Category;
import model.Item;

import java.math.BigDecimal;
import java.util.List;

public interface ItemDAO {
    Item create(String name, String description, BigDecimal price, Category category, int quantity, String imgFilepath);
    Item read(int id);
    Item read(String name, String description, Category category);
    Item update(Item item);
    List<Item> readByIndex(int index,String category, String priceOrder, String ratingOrder, String discountOrder, String statusOrder, String search);
    List<Item> readAllFromOrder(int orderId);
    List<Item> readAllByIds(int[] itemIds);
    List<Item> readCustomerWishlist(int customerId);
    void addWishlist(int customerId, int itemId);
    void removeItemFromWishlist(int customerId, int itemId);
    List<Item> readByItemName(String itemName, int index);
    void addToShoppingCart(Item item, int customerId);
    List<Item> readShoppingCart (int customerId);
    void updateShoppingCart (Item item, int customerId);
    void removeFromShoppingCart (Item item, int customerId);
}
