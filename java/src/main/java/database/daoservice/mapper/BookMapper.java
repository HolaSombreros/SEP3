package database.daoservice.mapper;

import database.daoservice.DataMapper;
import database.model.Book;
import database.model.enums.Category;
import database.model.enums.Genre;
import database.model.enums.ItemStatus;
import database.model.enums.Language;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.time.LocalDate;

public class BookMapper implements DataMapper<Book> {
    @Override
    public Book mapper(ResultSet resultSet) throws SQLException {
        return new Book(resultSet.getInt("item_id"),resultSet.getString("name"),resultSet.getString("description"),
                resultSet.getDouble("price"), Category.valueOf(resultSet.getString("category")),resultSet.getInt("quatity"),
                ItemStatus.valueOf(resultSet.getString("status")), resultSet.getInt("discount"), resultSet.getString("isbn"),
                resultSet.getString("author_first_name"),resultSet.getString("author_last_name"), Language.valueOf(resultSet.getString("language")),
                Genre.valueOf(resultSet.getString("genre")), resultSet.getDate("publication_date").toLocalDate());
    }
}
