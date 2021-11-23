package model;

public class Category {
    private String name;
    private int id;

    public Category(int id,String category) {
        this.id = id;
        this.name = category;
    }
    public Category(int id) {
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
        return "Id= " +  id + " Category= " + name;
    }
}
