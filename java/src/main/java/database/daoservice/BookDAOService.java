package database.daoservice;

import database.daomodel.BookDAO;
import database.model.Book;
import database.model.Item;
import database.model.enums.Genre;
import database.model.enums.Language;

import java.util.Date;

public class BookDAOService implements BookDAO {
    @Override
    public Book create(Item item, String ISBN, String authorFirstName, String authorLastName, Language language, Genre genre, Date publicationDate) {
        return null;
    }

    @Override
    public Book read(String ISBN) {
        return null;
    }

    @Override
    public void update(Book book) {

    }

    @Override
    public void delete(Book book) {

    }
}
