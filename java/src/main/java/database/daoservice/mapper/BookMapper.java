package database.daoservice.mapper;

import model.Book;
import model.Category;
import model.Genre;
import model.enums.ItemStatus;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

public class BookMapper implements DataMapper<Book> {
    @Override
    public Book map(ResultSet resultSet) throws SQLException {
        return new Book(resultSet.getInt("item_id"),resultSet.getString("name"),resultSet.getString("description"),
                resultSet.getDouble("price"), new Category(resultSet.getInt("category_id")),resultSet.getInt("quantity"),
                ItemStatus.fromString(resultSet.getString("status")), resultSet.getInt("discount"), resultSet.getString("image_filepath"), resultSet.getString("isbn"),
                null, resultSet.getString("language"),
                null, resultSet.getDate("publication_date").toLocalDate());
    }
}
