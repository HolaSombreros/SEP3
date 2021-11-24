package database.daoservice;

import database.daomodel.BookDAO;
import database.daoservice.mapper.BookMapper;
import database.daoservice.mapper.GenreMapper;
import model.Book;
import model.Category;
import model.Genre;
import model.Item;
import model.enums.ItemStatus;

import java.sql.SQLException;
import java.sql.Date;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;

public class BookDAOService implements BookDAO {

    private DatabaseHelper<Book> databaseHelper;
    private ItemDAOService itemDAOService;

    public BookDAOService(String url, String username, String password) {
        databaseHelper = new DatabaseHelper<>(url, username, password);
        itemDAOService = new ItemDAOService(url, username, password);
    }

    @Override
    public Book create(String name, String description, double price, Category category, int quantity, String imgFilePath, String ISBN, String authorFirstName, String authorLastName, String language, ArrayList<Genre> genre, LocalDate publicationDate) {
        try {
            List<Integer> genreKeys = null;
            if(!isBook(ISBN)) {
                for(Genre g : genre) {
                    if(!isGenre(g.getName()))
                        genreKeys = databaseHelper.executeUpdateWithKeys("INSERT INTO genre (name) VALUES (?)", g.getName());
                }
                Item item = itemDAOService.create(name,description,price,category,quantity,imgFilePath);
                databaseHelper.executeUpdate("INSERT INTO book (ISBN, item_id, author_first_name, author_last_name, language, publication_date) VALUES (?,?,?,?,?,?)",
                        ISBN, item.getId(), authorFirstName, authorLastName, language, publicationDate);
                for(Genre g : genre) {
                    databaseHelper.executeUpdateWithKeys("INSERT INTO book_genre VALUES (?,?)", item.getId(),g.getId());
                }
                return read(ISBN, item.getId());
            }
            return readByISBN(ISBN);

        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public Book read(String ISBN, int id) {
        try {
            Book book = databaseHelper.mapObject(new BookMapper(),"SELECT * FROM item,book WHERE book.item_id = ? AND ISBN = ?", id,ISBN);
            book.setGenre(getGenres(id));
            return book;
        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public Book read(int id) {
        try{
            Book book = databaseHelper.mapObject(new BookMapper(),"SELECT * FROM item,book WHERE book.item_id = ?", id);
            book.setGenre(getGenres(id));
            return book;
        }
        catch(SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public void update(Book book) {
        try {

            for(Genre g : book.getGenre()) {
                if(!isGenre(g.getName()))
                    databaseHelper.executeUpdateWithKeys("INSERT INTO genre (name) VALUES (?)", g.getName());
                if(!isInBookGenre(book.getId(), g.getId()))
                    databaseHelper.executeUpdateWithKeys("INSERT INTO book_genre VALUES (?,?)", g.getId(), book.getId());
            }
            databaseHelper.executeUpdate("UPDATE item SET name = ?, description = ?, price = ?, category_id = ?, quantity = ?, status = ?, discount =?, image_filepath = ? WHERE item_id = ?",
                    book.getName(), book.getDescription(), book.getPrice(), book.getCategory().getId(), book.getQuantity(), book.getStatus().toString(),
                    book.getDiscount(),book.getImageName(), book.getId());

            databaseHelper.executeUpdate("UPDATE book SET author_first_name = ?, author_last_name = ?, language = ?, publication_date = ? WHERE isbn = ?",
                    book.getAuthorFirstName(), book.getAuthorLastName(), book.getLanguage(), Date.valueOf(book.getPublicationDate().getLocalDate()),
                    book.getISBN());

        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public void delete(Book book) {

    }

    @Override
    public List<Book> readAll() {
        try {
            List<Book> books = databaseHelper.mapList(new BookMapper(), "SELECT * FROM book JOIN item USING (item_id);");
            for(Book book: books) {
                book.setGenre(getGenres(book.getId()));
            }
            return books;
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    private Book readByISBN(String ISBN){
        try {
            Book book = databaseHelper.mapObject(new BookMapper(),"SELECT * FROM book JOIN item USING (item_id) WHERE ISBN = ?;", ISBN);
            book.setGenre(getGenres(book.getId()));
            return book;
        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    private boolean isBook(String ISBN){
        try{
            return databaseHelper.executeQuery(databaseHelper.getConnection(), "SELECT * FROM book WHERE isbn = ?", ISBN).next();
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    private boolean isGenre(String genre) {
        try {
            return databaseHelper.executeQuery(databaseHelper.getConnection(),"SELECT * FROM genre WHERE name = ?", genre).next();
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }
    private List<Genre> getGenres(int id) {
        try {
            return databaseHelper.mapList(new GenreMapper(),"SELECT * FROM book_genre JOIN genre USING (genre_id) WHERE item_id = ?", id);
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }
    private boolean isInBookGenre(int item_id, int genre_id) {
        try {
            return databaseHelper.executeQuery(databaseHelper.getConnection(),"SELECT * FROM book_genre WHERE item_id = ? AND genre_id = ?", item_id, genre_id).next();
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

}
