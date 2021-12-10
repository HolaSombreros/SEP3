package model;

import model.enums.ItemStatus;
import java.math.BigDecimal;
import java.util.ArrayList;
import java.util.List;

public class Item {
    private int id;
    private String name;
    private String description;
    private BigDecimal price;
    private Category category;
    private int quantity;
    private ItemStatus status;
    private int discount;
    private String filePath;
    private List<Review> reviews;

    public Item(int id, String name, String description, BigDecimal price, Category category, int quantity, String filePath) {
        this.id = id;
        this.name = name;
        this.description = description;
        this.price = price;
        this.category = category;
        this.quantity = quantity;
        this.status = ItemStatus.INSTOCK;
        this.discount = 0;
        this.filePath = filePath;
        this.reviews = new ArrayList<>();
    }

    public Item(int id, String name, String description, BigDecimal price, Category category, int quantity, ItemStatus status, int discount, String filePath) {
        this.id = id;
        this.name = name;
        this.description = description;
        this.price = price;
        this.category = category;
        this.quantity = quantity;
        this.status = status;
        this.discount = discount;
        this.filePath = filePath;
    }

    public List<Review> getReviews() {
        return reviews;
    }

    public void setReviews(List<Review> reviews) {
        this.reviews = reviews;
    }

    public String getImageName() {
        return filePath;
    }

    public void setImageName(String imageName) {
        this.filePath = imageName;
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

    public BigDecimal getPrice() {
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

    public void setPrice(BigDecimal price) {
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
                category.equals(other.category) && quantity == other.quantity && status.equals(other.status) && discount == other.discount && filePath.equals(other.filePath);
    }
}
