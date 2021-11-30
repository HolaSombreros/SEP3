package database.daoservice;

import database.daomodel.GenreDAO;
import database.daoservice.mapper.GenreMapper;
import model.Genre;

import java.sql.SQLException;
import java.util.List;

public class GenreDAOService implements GenreDAO {
    private DatabaseHelper<Genre> databaseHelper;

    public GenreDAOService(String url, String username, String password) {
        databaseHelper = new DatabaseHelper<>(url, username, password);
    }

    @Override
    public Genre create(String genreName) {
        try {
            Genre genre = read(genreName);
            if (genre == null) {
                List<Integer> keys = databaseHelper.executeUpdateWithKeys("INSERT INTO genre (name) VALUES (?)", genreName);
                return read(keys.get(0));
            } else return genre;

        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public Genre read(int id) {
        try {
            return databaseHelper.mapObject(new GenreMapper(), "SELECT * FROM genre WHERE genre_id = ?", id);
        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public Genre read(String genreName) {
        try {
            return databaseHelper.mapObject(new GenreMapper(), "SELECT * FROM genre WHERE name = ?", genreName);
        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public Genre update(Genre genre) {
        try {
            if (read(genre.getId()) == null)
                return create(genre.getName());
            else {
                return read(genre.getId());
            }
        } catch (Exception e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public void updateBookGenre(Genre genre, int itemId) {
        try{
            Genre g = read(genre.getName());
            if(!isGenre(g.getName()))
                databaseHelper.executeUpdateWithKeys("INSERT INTO book_genre VALUES (?,?)", itemId,g.getId());
        }
        catch (SQLException e){
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public void delete(Genre genre) {

    }

    @Override
    public List<Genre> getGenresOfBook(int itemId) {
        try {
            return databaseHelper.mapList(new GenreMapper(), "SELECT * FROM book_genre JOIN genre USING (genre_id) WHERE item_id = ?", itemId);
        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public List<Genre> getAllGenres() {
        try {
            return databaseHelper.mapList(new GenreMapper(), "SELECT * FROM genre");
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    private boolean isGenre(String genre) {
        try {
            return databaseHelper.executeQuery(databaseHelper.getConnection(), "SELECT * FROM genre WHERE name = ?", genre).next();
        } catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }
}
