package model;

import model.enums.ItemStatus;

import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;

public class Book extends Item{

    private String ISBN;
    private String authorFirstName;
    private String authorLastName;
    private String language;
    private List<Genre> genre;
    private MyDateTime publicationDate;

    public Book(int id, String name, String description, double price, Category category, int quantity, ItemStatus status, int discount, String filePath, String ISBN, String authorFirstName, String authorLastName, String language, ArrayList<Genre> genre, MyDateTime publicationDate) {
        super(id, name, description, price, category, quantity, status, discount,filePath);
        this.ISBN = ISBN;
        this.authorFirstName = authorFirstName;
        this.authorLastName = authorLastName;
        this.language = language;
        this.genre = genre;
        this.publicationDate = publicationDate;
    }

    public Book(int id, String name, String description, double price, Category category, int quantity, ItemStatus status, int discount, String filePath, String ISBN, String authorFirstName, String authorLastName, String language, ArrayList<Genre> genre, LocalDate publicationDate) {
        super(id, name, description, price, category, quantity, status, discount,filePath);
        this.ISBN = ISBN;
        this.authorFirstName = authorFirstName;
        this.authorLastName = authorLastName;
        this.language = language;
        this.genre = genre;
        this.publicationDate = new MyDateTime(publicationDate.getYear(), publicationDate.getMonthValue(), publicationDate.getDayOfMonth(), 0, 0, 0);
    }

    public String getISBN() {
        return ISBN;
    }

    public String getAuthorFirstName() {
        return authorFirstName;
    }

    public String getAuthorLastName() {
        return authorLastName;
    }

    public String getLanguage() {
        return language;
    }

    public List<Genre> getGenre() {
        return genre;
    }

    public MyDateTime getPublicationDate() {
        return publicationDate;
    }

    @Override
    public String toString() {
        return "Book{" +
                "ISBN='" + ISBN + '\'' +
                ", authorFirstName='" + authorFirstName + '\'' +
                ", authorLastName='" + authorLastName + '\'' +
                ", language=" + language +
                ", genre=" + genre +
                ", publicationDate=" + publicationDate +
                '}';
    }

    public boolean equals(Object obj){
        if(!(obj instanceof Book))
            return false;
        Book other = (Book)obj;
        return super.equals(other) && ISBN.equals(other.ISBN) && authorFirstName.equals(other.authorFirstName) &&
                authorLastName.equals(other.authorLastName) && language.equals(other.language) && genre.equals(other.genre) && publicationDate.equals(other.publicationDate);
    }



}
