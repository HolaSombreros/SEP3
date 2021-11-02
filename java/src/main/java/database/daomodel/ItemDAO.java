package database.daomodel;

import database.daoservice.DataMapper;
import database.model.Item;
import database.model.enums.Category;

import java.util.Collection;
import java.util.List;

public interface ItemDAO {
    //CRUD metods
    Item create(String name, String description, double price, Category category, int quantity);
    Item read(int id);
    void update(Item item);
    void delete(Item item);
    List<Item> readByCategory(Category category);
    List<Item> readAll();
}
