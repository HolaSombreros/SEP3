package database.daomodel;

import database.model.Item;
import database.model.enums.Category;

public interface ItemDAO {
    //CRUD metods
    Item create(String name, String description, double price, Category category, int quantity);
    Item read(int id);
    void update(Item item);
    void delete(Item item);
}
