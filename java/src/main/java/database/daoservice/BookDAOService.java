package database.daoservice;

import database.daomodel.BookDAO;
import database.daoservice.mapper.BookMapper;
import database.daoservice.mapper.ItemMapper;
import database.model.Book;
import database.model.Item;
import database.model.enums.Category;
import database.model.enums.Genre;
import database.model.enums.ItemStatus;
import database.model.enums.Language;

import java.sql.SQLException;
import java.sql.Date;
import java.time.LocalDate;
import java.util.List;

public class BookDAOService implements BookDAO {

    private DatabaseHelper<Book> databaseHelper;

    public BookDAOService(String url, String username, String password) {
        databaseHelper = new DatabaseHelper<>(url, username, password);
    }

    @Override
    public Book create(String name, String description, double price, Category category, int quantity, String ISBN, String authorFirstName, String authorLastName, Language language, Genre genre, LocalDate publicationDate) {
        try {
            List<Integer> keys = databaseHelper.executeUpdateWithKeys("INSERT INTO item (name, description, price, category, discount, quantity, status) VALUES (?,?,?,?,?,?,?",
                    name, description,price,category.toString(),0,quantity, ItemStatus.INSTOCK);
            databaseHelper.executeUpdate("INSERT INTO book (ISBN, item_id, author_first_name, author_last_name, language, genre, publication_date) VALUES (?,?,?,?,?,?,?)",
                    ISBN,keys.get(0),authorFirstName,authorLastName,language,genre,publicationDate );
            return read(ISBN,keys.get(0));

        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public Book read(String ISBN, int id) {
        try {
            return databaseHelper.mapObject(new BookMapper(),"SELECT * FROM item,book WHERE id = ? AND ISBN = ?", id,ISBN);
        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public void update(Book book) {
        try {
            databaseHelper.executeUpdate("UPDATE item SET name = ?, description = ?, price = ?, category = ?, quantity = ?, status = ?, discount =? WHERE item_id = ?",
                    book.getName(), book.getDescription(), book.getPrice(), book.getCategory().toString(), book.getQuantity(), book.getStatus().toString(),
                    book.getDiscount(), book.getId());
            databaseHelper.executeUpdate("UPDATE book SET author_first_name = ?, author_last_name = ?, language = ?, genre = ?, publication_date = ? WHERE isbn = ?",
                    book.getAuthorFirstName(), book.getAuthorLastName(), book.getLanguage().toString(), book.getGenre().toString(), Date.valueOf(book.getPublicationDate()),
                    book.getISBN());

        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public void delete(Book book) {

    }
}
