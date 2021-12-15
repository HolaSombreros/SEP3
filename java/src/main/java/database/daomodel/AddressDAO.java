package database.daomodel;

import model.Address;

public interface AddressDAO {
    Address create(String street, String number, int zipcode, String city);
    Address read(int id);
    Address read(String street, String number, int zipcode);
}
