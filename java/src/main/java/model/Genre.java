package model;

public class Genre {
    private String name;
    private int id;

    public Genre(String genre, int id) {
        this.name = genre;
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String toString(){
        return "Genre= " + name + " id= " + id;
    }
}
