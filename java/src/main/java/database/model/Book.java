package database.model;

import database.model.enums.Category;
import database.model.enums.Genre;
import database.model.enums.Language;

import java.util.Date;

public class Book extends Item{

    private String ISBN;
    private String authorFirstName;
    private String authorLastName;
    private Language language;
    private Genre genre;
    private Date publicationDate;

    public Book(int id, String name, String description, double price, Category category, int quantity, String ISBN, String authorFirstName, String authorLastName, Language language, Genre genre, Date publicationDate) {
        super(id, name, description, price, category, quantity);
        this.ISBN = ISBN;
        this.authorFirstName = authorFirstName;
        this.authorLastName = authorLastName;
        this.language = language;
        this.genre = genre;
        this.publicationDate = publicationDate;
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

    public Language getLanguage() {
        return language;
    }

    public Genre getGenre() {
        return genre;
    }

    public Date getPublicationDate() {
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
