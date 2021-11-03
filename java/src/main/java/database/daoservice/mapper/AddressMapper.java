package database.daoservice.mapper;

import model.Address;

import java.sql.ResultSet;
import java.sql.SQLException;

public class AddressMapper implements DataMapper<Address> {
    @Override
    public Address map(ResultSet resultSet) throws SQLException {
        return new Address(resultSet.getString("street"), resultSet.getString("number"), resultSet.getInt("zip_code"),
                resultSet.getString("city"), resultSet.getInt("address_id"));
    }
}
