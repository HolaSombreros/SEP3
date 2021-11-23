package database.daomodel;

import model.Book;
import model.Category;
import model.Genre;

import java.time.LocalDate;
import java.util.List;

public interface BookDAO {
    Book create(String name, String description, double price, Category category, int quantity, String imgFilePath, String ISBN, String authorFirstName, String authorLastName, String language, Genre genre, LocalDate publicationDate);
    Book read(String ISBN, int id);
    Book read(int id);
    void update(Book book);
    void delete(Book book);
    List<Book> readAll();
}
