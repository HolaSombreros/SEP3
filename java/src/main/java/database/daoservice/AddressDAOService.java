package database.daoservice;

import database.DatabaseManager;
import database.daomodel.AddressDAO;
import database.daoservice.mapper.AddressMapper;
import database.daoservice.mapper.BookMapper;
import database.model.Address;
import database.model.Item;

import java.sql.SQLException;
import java.util.List;

public class AddressDAOService implements AddressDAO {

    DatabaseHelper<Address> databaseHelper;

    public AddressDAOService(String url, String username, String password) {
        databaseHelper = new DatabaseHelper<>(url, username, password);
    }

    @Override
    public Address create(String street, String number, int zipcode, String city) {
        try {
            List<Integer> keys = databaseHelper.executeUpdateWithKeys("INSERT INTO address (street, number, zip_code) " +
                            "VALUES (?,?,?)", street,number,zipcode);
            databaseHelper.executeUpdate("INSERT INTO city (zip_code,city) VALUES (?,?)",zipcode,city);
            return read(keys.get(0));

        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public Address read(int id) {
        try {
            return databaseHelper.mapObject(new AddressMapper(),"SELECT * FROM address JOIN city USING(zip_code))",id);
        } catch (SQLException e) {
//            throw new IllegalArgumentException(e.getMessage());
            return null;
        }
    }
}
