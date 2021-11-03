package database.daoservice;

import database.daomodel.AddressDAO;
import database.daoservice.mapper.AddressMapper;
import model.Address;

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
            databaseHelper.executeUpdate("INSERT INTO city (zip_code,city) VALUES (?,?)",zipcode,city);
            List<Integer> keys = databaseHelper.executeUpdateWithKeys("INSERT INTO address (street, number, zip_code) " +
                            "VALUES (?,?,?)", street,number,zipcode);
            return read(keys.get(0));

        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public Address read(int id) {
        try {
            System.out.println(id);
            return databaseHelper.mapObject(new AddressMapper(),"SELECT * FROM address JOIN city USING(zip_code) WHERE address_id = ?;", id);
        } catch (SQLException e) {
            e.printStackTrace();
            throw new IllegalArgumentException(e.getMessage());
            //return null;
        }
    }
}
