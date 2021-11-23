package model;

public class Genre {
    private String genre;
    private int id;

    public Genre(String genre, int id) {
        this.genre = genre;
        this.id = id;
    }

    public String getGenre() {
        return genre;
    }

    public void setGenre(String genre) {
        this.genre = genre;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String toString(){
        return "Genre= " + genre + " id= " + id;
    }
}
