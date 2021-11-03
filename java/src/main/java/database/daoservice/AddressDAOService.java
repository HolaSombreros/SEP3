package database.daoservice;

import database.daomodel.AddressDAO;
import database.daoservice.mapper.AddressMapper;
import model.Address;
import org.graalvm.compiler.core.common.type.ArithmeticOpTable;

import java.sql.SQLException;
import java.util.List;
import java.util.SplittableRandom;

public class AddressDAOService implements AddressDAO {

    DatabaseHelper<Address> databaseHelper;

    public AddressDAOService(String url, String username, String password) {
        databaseHelper = new DatabaseHelper<>(url, username, password);
    }

    @Override
    public Address create(String street, String number, int zipcode, String city) {
        try {
            if(!isCity(city,zipcode)) {
                databaseHelper.executeUpdate("INSERT INTO city (zip_code,city) VALUES (?,?)", zipcode, city);
            }
            if(!isAddress(street,number,zipcode,city)) {
                List<Integer> keys = databaseHelper.executeUpdateWithKeys("INSERT INTO address (street, number, zip_code) " +
                        "VALUES (?,?,?)", street, number, zipcode);
                return read(keys.get(0));
            }
            return read(street, number, zipcode, city);

        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public Address read(int id) {
        try {
            return databaseHelper.mapObject(new AddressMapper(),"SELECT * FROM address JOIN city USING(zip_code) WHERE address_id = ?;", id);
        } catch (SQLException e) {
//            e.printStackTrace();
            throw new IllegalArgumentException(e.getMessage());
            //return null;
        }
    }

    private boolean isAddress(String street, String number, int zipcode, String city){
        try{
            return databaseHelper.executeQuery(databaseHelper.getConnection(), "SELECT * FROM address WHERE zipcode = ?, street = ?, number = ?, city = ?", zipcode, street, number, city).next();
        }catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    private boolean isCity(String city, int zipcode){
        try{
            return databaseHelper.executeQuery(databaseHelper.getConnection(), "SELECT * FROM city WHERE zipcode = ?", zipcode).next();
        }catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    private Address read(String street, String number, int zipcode, String city){
        try{
            return databaseHelper.mapObject(new AddressMapper(),"SELECT * FROM address WHERE zipcode = ?, street = ?, number = ?, city = ?",zipcode, street, number, city );
        }catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }
}
