package database.daomodel;

import database.model.Book;
import database.model.Item;
import database.model.enums.Category;
import database.model.enums.Genre;
import database.model.enums.Language;

import java.time.LocalDate;


public interface BookDAO {
    Book create(String name, String description, double price, Category category, int quantity, String ISBN, String authorFirstName, String authorLastName, Language language, Genre genre, LocalDate publicationDate);
    Book read(String ISBN, int id);
    void update(Book book);
    void delete(Book book);
}
