package database.daoservice.mapper;

import model.Genre;

import java.sql.ResultSet;
import java.sql.SQLException;

public class GenreMapper implements DataMapper<Genre> {

    @Override
    public Genre map(ResultSet resultSet) throws SQLException {
        return new Genre(resultSet.getString("name"), resultSet.getInt("genre_id"));
    }
}
