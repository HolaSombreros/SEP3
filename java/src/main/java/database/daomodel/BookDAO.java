package database.daomodel;

import database.model.Book;
import database.model.Item;
import database.model.enums.Genre;
import database.model.enums.Language;

import java.util.Date;

public interface BookDAO {
    Book create(Item item, String ISBN, String authorFirstName, String authorLastName, Language language, Genre genre, Date publicationDate);
    Book read(String ISBN);
    void update(Book book);
    void delete(Book book);
}
