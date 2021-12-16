package database.daomodel;

import model.Genre;

import java.util.List;

public interface GenreDAO {
    Genre create(String genreName);
    Genre read(int id);
    Genre read(String genreName);
    Genre update(Genre genre);
    void updateBookGenre(Genre genre, int itemId);
    List<Genre> getGenresOfBook(int itemId);
    List<Genre> getAllGenres();
    void deleteBookGenre(int itemId);
}
