package database.daomodel;

import model.Address;
import model.Customer;
import model.Item;

import java.util.List;

public interface CustomerDAO {
    Customer create(String firstName, String lastName, String email, String password, String role, Address address, String phoneNumber);
    Customer read(int id);
    Customer read(String email);
    void update(Customer customer);
    void delete(Customer customer);
}
