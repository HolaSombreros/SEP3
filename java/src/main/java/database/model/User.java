package database.model;

public class User {
    private String firstName;
    private String lastName;
    private String middleName;
    private String email;

    public User(String firstName, String lastName, String middleName, String email) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.middleName = middleName;
        this.email = email;
    }

    public User(String firstName, String lastName, String email) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.middleName = null;
        this.email = email;
    }

}
