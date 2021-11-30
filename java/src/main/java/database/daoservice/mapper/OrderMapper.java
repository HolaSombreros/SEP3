package database.daoservice.mapper;

import model.Address;
import model.MyDateTime;
import model.Order;
import model.enums.OrderStatus;

import java.sql.ResultSet;
import java.sql.SQLException;

public class OrderMapper implements DataMapper<Order> {
    @Override
    public Order map(ResultSet resultSet) throws SQLException {
        return new Order(null, resultSet.getString("first_name"),resultSet.getString("last_name"),resultSet.getString("email"),
                resultSet.getInt("purchase_id"), new Address(resultSet.getString("street"), resultSet.getString("number"), resultSet.getInt("zip_code"),
                resultSet.getString("city"), resultSet.getInt("address_id")), new MyDateTime(resultSet.getTimestamp("date_time").toLocalDateTime()), OrderStatus.fromString(resultSet.getString("status")), resultSet.getInt("customer_id"));
    }
}


