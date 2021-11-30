package database.daomodel;

import model.Category;

import java.util.List;

public interface CategoryDAO {
    List<Category> readAllCategories();
    Category read(int categoryId);
    Category read(String categoryName);
    Category createCategory(String name);
}
