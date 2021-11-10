package database.daoservice.mapper;

import model.Address;
import model.Customer;

import java.sql.ResultSet;
import java.sql.SQLException;

public class CustomerMapper implements DataMapper<Customer> {
    @Override public Customer map(ResultSet resultSet) throws SQLException {
        return new Customer(resultSet.getInt("customer_id"), resultSet.getString("first_name"), resultSet.getString("last_name"), resultSet.getString("email"), resultSet.getString("password"),
            resultSet.getString("role"), new Address(resultSet.getString("street"), resultSet.getString("number"), resultSet.getInt("zip_code"),
            resultSet.getString("city"), resultSet.getInt("address_id")), resultSet.getString("phone_number"));
    }
}
