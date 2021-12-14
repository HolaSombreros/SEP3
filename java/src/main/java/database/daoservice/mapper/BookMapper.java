package database.daoservice.mapper;

import model.Book;
import model.Category;
import model.enums.ItemStatus;

import java.sql.ResultSet;
import java.sql.SQLException;


public class BookMapper implements DataMapper<Book> {
    /**
     * Maps and returns a book object based on the values provided by the result set passed as an argument
     * @param resultSet
     * @return
     * @throws SQLException
     */
    @Override
    public Book map(ResultSet resultSet) throws SQLException {
        return new Book(resultSet.getInt("item_id"),resultSet.getString("item_name"),resultSet.getString("description"),
                resultSet.getBigDecimal("price"), new Category(resultSet.getInt("category_id"),resultSet.getString("category_name")),resultSet.getInt("quantity"),
                ItemStatus.fromString(resultSet.getString("status")), resultSet.getInt("discount"), resultSet.getString("image_filepath"), resultSet.getString("isbn"),
                null, resultSet.getString("language"),
                null, resultSet.getDate("publication_date").toLocalDate());
    }
}
