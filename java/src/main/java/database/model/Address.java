package database.model;

public class Address {
    private String street;
    private String number;
    private int zipcode;
    private String city;

    public Address(String street, String number, int zipcode, String city) {
        setStreet(street);
        setNumber(number);
        setZipcode(zipcode);
        setCity(city);
    }
    public String getStreet() {
        return street;
    }

    public void setStreet(String street) {
        this.street = street;
    }

    public String getNumber() {
        return number;
    }

    public void setNumber(String number) {
        this.number = number;
    }

    public int getZipcode() {
        return zipcode;
    }

    public void setZipcode(int zipcode) {
        this.zipcode = zipcode;
    }

    public String getCity() {
        return city;
    }

    public void setCity(String city) {
        this.city = city;
    }

    public Address copy() {
        return new Address(street, number, zipcode, city);
    }

    public boolean equals(Object obj) {
        if (!(obj instanceof Address))
            return false;

        Address other = (Address) obj;
        return street.equals(other.street) && city.equals(other.city) && number.equals(other.number) && zipcode == other.zipcode;
    }

    public String toString() {
        return getZipcode() + " - " + getCity() + ": " + getStreet() + " " + getNumber();
    }
}
