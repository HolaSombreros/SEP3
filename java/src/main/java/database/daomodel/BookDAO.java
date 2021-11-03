package database.daomodel;

import database.model.Book;
import database.model.Item;
import database.model.MyDateTime;
import database.model.enums.Category;
import database.model.enums.Genre;
import database.model.enums.Language;

import java.time.LocalDate;
import java.util.Collection;
import java.util.List;


public interface BookDAO {
    Book create(String name, String description, double price, Category category, int quantity, String ISBN, String authorFirstName, String authorLastName, Language language, Genre genre, MyDateTime publicationDate);
    Book read(String ISBN, int id);
    void update(Book book);
    void delete(Book book);
    Collection<Book> readAll();
}
