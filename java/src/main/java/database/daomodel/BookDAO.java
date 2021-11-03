package database.daomodel;

import model.Book;
import model.enums.Category;
import model.enums.Genre;
import model.enums.Language;

import java.time.LocalDate;
import java.util.Collection;
import java.util.List;

public interface BookDAO {
    Book create(String name, String description, double price, Category category, int quantity, String ISBN, String authorFirstName, String authorLastName, Language language, Genre genre, LocalDate publicationDate);
    Book read(String ISBN, int id);
    Book read(int id);
    void update(Book book);
    void delete(Book book);
    List<Book> readAll();
}
