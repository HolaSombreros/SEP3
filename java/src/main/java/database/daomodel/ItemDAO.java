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
}
