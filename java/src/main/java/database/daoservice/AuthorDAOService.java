package database.daoservice;

import database.daomodel.AuthorDAO;
import database.daoservice.mapper.AuthorMapper;
import model.Author;
import model.Book;

import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

public class AuthorDAOService implements AuthorDAO {
    private DatabaseHelper<Author> databaseHelper;

    public AuthorDAOService(String url, String username, String password) {
        databaseHelper = new DatabaseHelper<>(url, username, password);
    }

    @Override
    public Author create(String firsName, String lastName) {
        try{
            Author author = read(firsName,lastName);
            if(author == null){
                List<Integer> keys = databaseHelper.executeUpdateWithKeys("INSERT INTO author(first_name, last_name) VALUES (?,?);",firsName,lastName);
                return read(keys.get(0));
            }
            else return author;

        }
        catch (SQLException e){
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public Author read(int id) {
        try {
            return databaseHelper.mapObject(new AuthorMapper(), "SELECT * FROM author WHERE author_id = ?;", id);
        }
        catch (SQLException e){
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public Author read(String firstName, String lastName) {
        try {
            return databaseHelper.mapObject(new AuthorMapper(), "SELECT * FROM author WHERE first_name = ? AND last_name = ?;", firstName, lastName);
        }
        catch (SQLException e){
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public void updateBookAuthor(Author author, int itemId) {
        try{
            databaseHelper.executeUpdateWithKeys("INSERT INTO book_author VALUES (?,?)", itemId,author.getId());
        }
        catch (SQLException e){
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public Author update(Author author) {
        try {
            if (read(author.getId()) == null)
                return create(author.getFirstName(), author.getLastName());
            else {
                databaseHelper.executeUpdate("UPDATE author SET first_name = ?, last_name = ?", author.getFirstName(), author.getLastName());
                return read(author.getId());
            }
        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public List<Author> readAllAuthorsOfBook(int itemId) {
        try{
            return databaseHelper.mapList(new AuthorMapper(),"SELECT * FROM author JOIN book_author USING (author_id) WHERE item_id = ?",itemId);
        }catch (SQLException e){
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public void delete(Author author) {

    }
}

