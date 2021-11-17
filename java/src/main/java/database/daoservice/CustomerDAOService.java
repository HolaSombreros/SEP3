package database.daoservice;

import database.daomodel.CustomerDAO;
import database.daoservice.mapper.CustomerMapper;
import database.daoservice.mapper.ItemMapper;
import model.Address;
import model.Customer;
import model.Item;

import java.sql.SQLException;
import java.util.List;

public class CustomerDAOService implements CustomerDAO {

    private DatabaseHelper<Customer> databaseHelper;
    private AddressDAOService addressDAOService;

    public CustomerDAOService(String url, String username, String password) {
        this.databaseHelper = new DatabaseHelper<>(url, username, password);
        addressDAOService = new AddressDAOService(url, username, password);
    }

    @Override public Customer create(String firstName, String lastName, String email, String password, String role, Address address, String phoneNumber) {
        try {
            Address address1;
            if(!addressDAOService.isAddress(address.getStreet(), address.getNumber(), address.getZipCode()))
                address1 = addressDAOService.create(address.getStreet(), address.getNumber(), address.getZipCode(), address.getCity());
            else
                address1 = addressDAOService.read(address.getStreet(), address.getNumber(), address.getZipCode());
            if(isEmail(email)){
                throw new IllegalArgumentException("This email is already registered");
            }
            List<Integer> keys = databaseHelper.executeUpdateWithKeys(
                    "INSERT INTO customer(first_name, last_name, email, password, role, address_id, phone_number) VALUES (?,?,?,?,?::user_role,?,?);", firstName, lastName, email, password, role,
                    address1.getId(), phoneNumber);
            return read(keys.get(0));
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override public Customer read(int id) {
        try {
            return databaseHelper.mapObject(new CustomerMapper(), "SELECT * FROM customer JOIN (SELECT * FROM address JOIN city USING (zip_code)) a USING (address_id) WHERE customer_id = ?;", id);
        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override public Customer read(String email) {
        try {
            return databaseHelper.mapObject(new CustomerMapper(), "SELECT * FROM customer JOIN (SELECT * FROM address JOIN city USING (zip_code)) a USING (address_id) WHERE email = ?;", email);
        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override public void update(Customer customer) {

    }

    @Override public void delete(Customer customer) {

    }

    private boolean isEmail(String email){
        try{
            return databaseHelper.executeQuery(databaseHelper.getConnection(), "SELECT * FROM customer WHERE email = ?", email).next();
        }catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }
}
