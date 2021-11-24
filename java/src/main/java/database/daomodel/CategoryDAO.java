package database.daomodel;

import model.Category;

import java.util.List;

public interface CategoryDAO {
    List<Category> readAllCategories();

}
