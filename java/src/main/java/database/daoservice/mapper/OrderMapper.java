package database.daoservice.mapper;

import database.daoservice.DataMapper;
import database.model.Address;
import database.model.MyDateTime;
import database.model.Order;
import database.model.User;
import database.model.enums.OrderStatus;

import java.sql.Date;
import java.sql.ResultSet;
import java.sql.SQLException;

public class OrderMapper implements DataMapper<Order> {
    @Override
    public Order map(ResultSet resultSet) throws SQLException {
        return new Order(null, new User(resultSet.getString("first_name"),resultSet.getString("last_name"), resultSet.getString("middle_name"),resultSet.getString("email")),
                resultSet.getInt("order_id"), new Address(resultSet.getString("street"), resultSet.getString("number"), resultSet.getInt("zip_code"),
                resultSet.getString("city"), resultSet.getInt("address_id")), new MyDateTime(resultSet.getTimestamp("date_time").toLocalDateTime()), OrderStatus.fromString(resultSet.getString("status")));
    }
}
