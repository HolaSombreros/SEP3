package database.daoservice;

import database.daomodel.AuthorDAO;
import database.daomodel.BookDAO;
import database.daomodel.GenreDAO;
import database.daomodel.ItemDAO;
import database.daoservice.mapper.BookMapper;
import model.*;

import java.sql.SQLException;
import java.sql.Date;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;

public class BookDAOService implements BookDAO {

    private DatabaseHelper<Book> databaseHelper;
    private ItemDAO itemDAOService;
    private AuthorDAO authorDAOService;
    private GenreDAO genreDAOService;

    public BookDAOService(String url, String username, String password) {
        databaseHelper = new DatabaseHelper<>(url, username, password);
        itemDAOService = new ItemDAOService(url, username, password);
        authorDAOService = new AuthorDAOService(url, username, password);
        genreDAOService = new GenreDAOService(url, username, password);
    }

    @Override
    public Book create(String name, String description, double price, Category category, int quantity, String imgFilePath, String ISBN, List<Author> authors, String language, List<Genre> genre, LocalDate publicationDate) {
        try {
            for(Author author: authors){
                Author a = authorDAOService.create(author.getFirstName(), author.getLastName());
                author.setId(a.getId());
            }
            for(Genre g: genre)
                genreDAOService.create(g.getName());
            if(!isBook(ISBN)) {
                Item item = itemDAOService.create(name,description,price,category,quantity,imgFilePath);
                databaseHelper.executeUpdate("INSERT INTO book (ISBN, item_id, language, publication_date) VALUES (?,?,?,?)",
                        ISBN, item.getId(), language, publicationDate);
                for(Genre g : genre) {
                    genreDAOService.updateBookGenre(g,item.getId());
                }
                for(Author author: authors){
                    authorDAOService.updateBookAuthor(author,item.getId());
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
            System.out.println(id);
            Book book = databaseHelper.mapObject(new BookMapper(),"SELECT *,item.name AS item_name, category.name AS category_name FROM book JOIN item USING(item_id) JOIN category USING(category_id) WHERE book.item_id = ? AND ISBN = ?", id,ISBN);
            book.setGenre(genreDAOService.getGenresOfBook(id));
            book.setAuthors(authorDAOService.readAllAuthorsOfBook(id));
            return book;
        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public Book read(int id) {
        try{
            Book book = databaseHelper.mapObject(new BookMapper(),"SELECT *, category.name AS category_name, item.name AS item_name FROM book JOIN item USING(item_id) JOIN category USING(category_id) WHERE item_id = ?", id);
            System.out.println(book.getName());
            book.setGenre(genreDAOService.getGenresOfBook(id));
            book.setAuthors(authorDAOService.readAllAuthorsOfBook(id));
            return book;
        }
        catch(SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    /**
     * Read method used only to check if the database contains the ISBN specified by the admin on purpose of book creation
     * It does not provide more information than the ISBN
     * Do not use if you need more information about the item, genre or authors
     * @param ISBN
     * @return
     */
    @Override
    public Book read(String ISBN) {
        try {
            Book book = databaseHelper.mapObject(new BookMapper(), "SELECT * FROM book WHERE ISBN = ?", ISBN);
            return book;
        }
        catch(SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public Book update(Book book) {
        try {

            for(Genre g : book.getGenre()) {
                genreDAOService.update(g);
                if(!isInBookGenre(book.getId(), g.getId()))
                    genreDAOService.updateBookGenre(g,book.getId());
            }
            databaseHelper.executeUpdate("UPDATE item SET name = ?, description = ?, price = ?, category_id = ?, quantity = ?, status = ?::item_status, discount =?, image_filepath = ? WHERE item_id = ?",
                    book.getName(), book.getDescription(), book.getPrice(), book.getCategory().getId(), book.getQuantity(), book.getStatus().toString(),
                    book.getDiscount(),book.getImageName(), book.getId());

            databaseHelper.executeUpdate("UPDATE book SET language = ?, publication_date = ? WHERE isbn = ?",
                    book.getLanguage(), Date.valueOf(book.getPublicationDate().getLocalDate()),
                    book.getISBN());
            for(Author author: book.getAuthors()){
                authorDAOService.update(author);
            }
            return read(book.getISBN(), book.getId());
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
                book.setGenre(genreDAOService.getGenresOfBook(book.getId()));
                book.setAuthors(authorDAOService.readAllAuthorsOfBook(book.getId()));
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
            book.setGenre(genreDAOService.getGenresOfBook(book.getId()));
            book.setAuthors(authorDAOService.readAllAuthorsOfBook(book.getId()));
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


    private boolean isInBookGenre(int item_id, int genre_id) {
        try {
            return databaseHelper.executeQuery(databaseHelper.getConnection(),"SELECT * FROM book_genre WHERE item_id = ? AND genre_id = ?", item_id, genre_id).next();
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

}
