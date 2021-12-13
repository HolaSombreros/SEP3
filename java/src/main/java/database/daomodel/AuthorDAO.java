package database.daomodel;

import model.Author;
import java.util.List;

public interface AuthorDAO {
    Author create(String firsName, String lastName);
    Author read(int id);
    Author read(String firstName, String lastName);
    void updateBookAuthor(Author author, int itemId);
    Author update(Author author);
    List<Author> readAllAuthorsOfBook(int itemId);


}
