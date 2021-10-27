package database.daoservice.mapper;

import database.daoservice.DataMapper;
import database.model.Order;

import java.sql.ResultSet;
import java.sql.SQLException;

public class OrderMapper implements DataMapper<Order> {
    @Override
    public Order map(ResultSet resultSet) throws SQLException {
        return null;
    }
}
