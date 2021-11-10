package database.daomodel;

import model.Address;

public interface AddressDAO {
    Address create(String street, String number, int zipcode, String city);
    Address read(int id);
    boolean isAddress(String street, String number, int zipcode);
    Address read(String street, String number, int zipcode);
}
