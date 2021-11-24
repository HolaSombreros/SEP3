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
}
