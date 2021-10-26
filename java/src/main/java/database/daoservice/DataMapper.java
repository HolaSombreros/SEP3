package database.daoservice;

import java.sql.ResultSet;
import java.sql.SQLException;

public interface DataMapper<T> {
    T mapper(ResultSet resultSet) throws SQLException;
}

