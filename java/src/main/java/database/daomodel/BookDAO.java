package database.daomodel;

import model.*;

import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;

public interface BookDAO {
    Book create(String name, String description, double price, Category category, int quantity,
                String imgFilePath, String ISBN, List<Author> authors, String language,
                List<Genre> genre, LocalDate publicationDate);
    Book read(String ISBN, int id);
    Book read(int id);
    Book read(String ISBN);
    Book update(Book book);
    void delete(Book book);
    List<Book> readAll();
}
