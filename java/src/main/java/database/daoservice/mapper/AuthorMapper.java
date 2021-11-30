package database.daoservice.mapper;

import model.Author;

import java.sql.ResultSet;
import java.sql.SQLException;

public class AuthorMapper implements DataMapper<Author> {
    @Override
    public Author map(ResultSet resultSet) throws SQLException {
        return new Author(resultSet.getInt("author_id"), resultSet.getString("first_name"), resultSet.getString("last_name"));
    }
}
