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
    //TODO: Arraylist to array
    @Override
    public Book map(ResultSet resultSet) throws SQLException {
        return new Book(resultSet.getInt("item_id"),resultSet.getString("name"),resultSet.getString("description"),
                resultSet.getDouble("price"), new Category(resultSet.getInt("category")),resultSet.getInt("quantity"),
                ItemStatus.fromString(resultSet.getString("status")), resultSet.getInt("discount"), resultSet.getString("image_filepath"), resultSet.getString("isbn"),
                resultSet.getString("author_first_name"),resultSet.getString("author_last_name"), resultSet.getString("language"),
                new ArrayList<Genre>((Collection<Genre>) resultSet.getArray("genre_id")), resultSet.getDate("publication_date").toLocalDate());
    }
}
