package model;

import model.enums.ItemStatus;

import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;

public class Book extends Item{

    private String ISBN;
    private List<Author> authors;
    private String language;
    private List<Genre> genre;
    private MyDateTime publicationDate;

    public Book(int id, String name, String description, double price, Category category, int quantity, ItemStatus status, int discount, String filePath, String ISBN,ArrayList<Author> authors , String language, ArrayList<Genre> genre, MyDateTime publicationDate) {
        super(id, name, description, price, category, quantity, status, discount,filePath);
        this.ISBN = ISBN;
        this.authors = authors;
        this.language = language;
        this.genre = genre;
        this.publicationDate = publicationDate;
    }

    public Book(int id, String name, String description, double price, Category category, int quantity, ItemStatus status, int discount, String filePath, String ISBN, ArrayList<Author> authors, String language, ArrayList<Genre> genre, LocalDate publicationDate) {
        super(id, name, description, price, category, quantity, status, discount,filePath);
        this.ISBN = ISBN;
        this.authors = authors;
        this.language = language;
        this.genre = genre;
        this.publicationDate = new MyDateTime(publicationDate.getYear(), publicationDate.getMonthValue(), publicationDate.getDayOfMonth(), 0, 0, 0);
    }

    public String getISBN() {
        return ISBN;
    }

    public List<Author> getAuthors() {
        return authors;
    }

    public void setAuthors(List<Author> authors) {
        this.authors = authors;
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

    public void setGenre(List<Genre> genre) {
        this.genre = genre;
    }

    @Override
    public String toString() {
        String authorsString = "Authors = ";
        for(Author author:authors)
            authorsString += author.toString();
        return "Book{" +
                "ISBN='" + ISBN + '\'' +
                authorsString +
                ", language=" + language +
                ", genre=" + genre +
                ", publicationDate=" + publicationDate +
                '}';
    }

    public boolean equals(Object obj){
        if(!(obj instanceof Book))
            return false;
        Book other = (Book)obj;
        for(int i = 0; i < authors.size(); i++)
            if(!authors.get(i).equals(other.getAuthors().get(i)))
                return false;
        return super.equals(other) && ISBN.equals(other.ISBN) && language.equals(other.language) && genre.equals(other.genre) && publicationDate.equals(other.publicationDate);
    }



}
