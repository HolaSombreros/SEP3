package database.daoservice.mapper;

import model.Category;

import java.sql.ResultSet;
import java.sql.SQLException;

public class CategoryMapper implements DataMapper<Category> {

    @Override
    public Category map(ResultSet resultSet) throws SQLException {
        return new Category(resultSet.getInt("category_id"), resultSet.getString("name"));
    }
}
