package database.daoservice.mapper;

import model.Book;
import model.enums.Category;
import model.enums.Genre;
import model.enums.ItemStatus;
import model.enums.Language;

import java.sql.ResultSet;
import java.sql.SQLException;

public class BookMapper implements DataMapper<Book> {
    @Override
    public Book map(ResultSet resultSet) throws SQLException {
        return new Book(resultSet.getInt("item_id"),resultSet.getString("name"),resultSet.getString("description"),
                resultSet.getDouble("price"), Category.fromString(resultSet.getString("category")),resultSet.getInt("quantity"),
                ItemStatus.fromString(resultSet.getString("status")), resultSet.getInt("discount"), resultSet.getString("isbn"),
                resultSet.getString("author_first_name"),resultSet.getString("author_last_name"), Language.fromString(resultSet.getString("language")),
                Genre.fromString(resultSet.getString("genre")), resultSet.getDate("publication_date").toLocalDate());
    }
}
