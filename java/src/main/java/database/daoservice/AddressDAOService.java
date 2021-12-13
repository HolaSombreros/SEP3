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
            if(!isCity(city,zipcode)) {
                if(!isZipcode(zipcode))
                    databaseHelper.executeUpdate("INSERT INTO city (zip_code,city) VALUES (?,?)", zipcode, city);
                else
                    throw new IllegalArgumentException("City and zipcode do not match");
            }
            if(!isAddress(street,number,zipcode)) {
                List<Integer> keys = databaseHelper.executeUpdateWithKeys("INSERT INTO address (street, number, zip_code) " +
                        "VALUES (?,?,?)", street, number, zipcode);
                return read(keys.get(0));
            }
            return read(street, number, zipcode, city);

        }
        catch (Exception e) {
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

    public boolean isAddress(String street, String number, int zipcode){
        try {
            return databaseHelper.executeQuery(databaseHelper.getConnection(), "SELECT * FROM address WHERE zip_code = ? AND street = ? AND number = ?", zipcode, street, number).next();
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public Address read(String street, String number, int zipcode) {
        try {
            return databaseHelper.mapObject(new AddressMapper(),"SELECT * FROM address JOIN city USING(zip_code) WHERE street = ? AND number = ? AND zip_code = ?;", street,number, zipcode);
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    private boolean isCity(String city, int zipcode){
        try  {
            return databaseHelper.executeQuery(databaseHelper.getConnection(), "SELECT * FROM city WHERE zip_code = ? AND city = ?", zipcode, city).next();
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    private boolean isZipcode(int zipcode) {
        try {
            return databaseHelper.executeQuery(databaseHelper.getConnection(), "SELECT * FROM city WHERE zip_code = ?", zipcode).next();
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    private Address read(String street, String number, int zipcode, String city){
        try {
            return databaseHelper.mapObject(new AddressMapper(),"SELECT * FROM address JOIN city USING (zip_code) WHERE zip_code = ? AND street = ? AND number = ? AND city = ?",zipcode, street, number, city );
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }
}
