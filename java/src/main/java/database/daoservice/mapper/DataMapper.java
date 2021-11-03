package database.daoservice.mapper;

import java.sql.ResultSet;
import java.sql.SQLException;

public interface DataMapper<T> {
    T map(ResultSet resultSet) throws SQLException;
}

