package model;

import model.enums.Category;
import model.enums.ItemStatus;

public class Item {

    private int id;
    private String name;
    private String description;
    private double price;
    private Category category;
    private int quantity;
    private ItemStatus status;
    //TODO discuss discount
    private int discount;

    public Item() {

    }

    public Item(int id, String name, String description, double price, Category category, int quantity) {
        this.id = id;
        this.name = name;
        this.description = description;
        this.price = price;
        this.category = category;
        this.quantity = quantity;
        this.status = ItemStatus.INSTOCK;
        this.discount = 0;
    }

    public Item(int id, String name, String description, double price, Category category, int quantity, ItemStatus status, int discount) {
        this.id = id;
        this.name = name;
        this.description = description;
        this.price = price;
        this.category = category;
        this.quantity = quantity;
        this.status = status;
        this.discount = discount;
    }

    public int getId() {
        return id;
    }

    public String getName() {
        return name;
    }

    public String getDescription() {
        return description;
    }

    public double getPrice() {
        return price;
    }

    public Category getCategory() {
        return category;
    }

    public int getQuantity() {
        return quantity;
    }

    public ItemStatus getStatus() {
        return status;
    }

    public int getDiscount() {
        return discount;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public void setPrice(double price) {
        this.price = price;
    }

    public void setQuantity(int quantity) {
        this.quantity = quantity;
    }

    public void setStatus(ItemStatus status) {
        this.status = status;
    }

    public void setDiscount(int discount) {
        this.discount = discount;
    }

    @Override
    public String toString() {
        return
                "Name='" + name + '\'' +
                ", description='" + description + '\'' +
                ", price=" + price +
                ", category=" + category +
                ", quantity=" + quantity +
                ", status=" + status +
                ", discount=" + discount;
    }

    public boolean equals(Object obj){
        if(!(obj instanceof Item))
            return false;
        Item other = (Item) obj;
        return other.id == id  && name.equals(other.name) && description.equals(other.description) && price == other.price &&
                category.equals(other.category) && quantity == other.quantity && status.equals(other.status) && discount == other.discount;
    }
}
