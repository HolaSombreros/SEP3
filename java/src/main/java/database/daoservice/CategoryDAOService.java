package database.daoservice;

import database.daomodel.CategoryDAO;
import database.daoservice.mapper.CategoryMapper;
import model.Category;

import java.sql.SQLException;
import java.util.List;

public class CategoryDAOService implements CategoryDAO {

    private DatabaseHelper<Category> databaseHelper;

    public CategoryDAOService(String url,String username,String password) {
        databaseHelper = new DatabaseHelper<>(url, username, password);
    }

    @Override
    public List<Category> readAllCategories() {
        try {
            List<Category> categories = databaseHelper.mapList(new CategoryMapper(), "SELECT * FROM category;");
            return categories;
        }
        catch (SQLException e){
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override public Category read(int categoryId) {
        try {
            Category category = databaseHelper.mapObject(new CategoryMapper(), "SELECT * FROM category WHERE category_id = ?;", categoryId);
            return category;
        } catch (SQLException e) {
            throw new IllegalStateException(e.getMessage());
        }
    }

    @Override public Category createCategory(String name) {
        try {
            List<Integer> keys = databaseHelper.executeUpdateWithKeys("INSERT INTO category (name) VALUES (?);", name);
            return read(keys.get(0));
        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }
}
