package database.daomodel;

import database.model.Address;

public interface AddressDAO {
    Address create(String street, String number, int zipcode, String city);
    Address read(int id);
}
