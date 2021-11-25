package database.daomodel;

import model.Author;
import model.Book;
import model.Category;
import model.Genre;

import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;

public interface BookDAO {
    Book create(String name, String description, double price, Category category, int quantity, String imgFilePath, String ISBN, ArrayList<Author> authors, String language, ArrayList<Genre> genre, LocalDate publicationDate);
    Book read(String ISBN, int id);
    Book read(int id);
    Book update(Book book);
    void delete(Book book);
    List<Book> readAll();
}
