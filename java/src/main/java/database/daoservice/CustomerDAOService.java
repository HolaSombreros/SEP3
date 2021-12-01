package database.daoservice;

import database.daomodel.CustomerDAO;
import database.daoservice.mapper.CustomerMapper;
import database.daoservice.mapper.ItemMapper;
import database.daoservice.mapper.OrderMapper;
import model.Address;
import model.Customer;
import model.Item;
import model.Order;

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

    @Override
    public List<Customer> readByIndex(int index) {
        try {
            return databaseHelper.mapList(new CustomerMapper(), "SELECT * FROM customer JOIN (SELECT * FROM address JOIN city USING (zip_code)) a USING (address_id) ORDER BY customer_id ASC LIMIT 21 OFFSET 21 * ?", index);
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override public Customer update(Customer customer) {
        try {
            Address address = addressDAOService.read(customer.getAddress().getStreet(),
                    customer.getAddress().getNumber(), customer.getAddress().getZipCode());
            if (address == null) {
                address = addressDAOService.create(customer.getAddress().getStreet(), customer.getAddress().getNumber(),
                        customer.getAddress().getZipCode(), customer.getAddress().getCity());
            }

            customer.setAddress(address);
            databaseHelper.executeUpdate("UPDATE customer SET first_name = ?, last_name = ?, email = ?,"
                            + " password = ?, role = ?::user_role, phone_number = ?, address_id = ? WHERE customer_id = ?;",
                    customer.getFirstName(), customer.getLastName(), customer.getEmail(), customer.getPassword(),
                    customer.getRole(), customer.getPhoneNumber(), customer.getAddress().getId(), customer.getId());

            return customer;
        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public Customer updateRole(Customer customer) {
        try {
            databaseHelper.executeUpdate("UPDATE customer SET role = ?::user_role WHERE customer_id = ?",customer.getRole(), customer.getId());
            return customer;
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
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
