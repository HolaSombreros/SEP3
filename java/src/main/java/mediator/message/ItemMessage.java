package mediator.message;

import model.*;
import java.util.ArrayList;
import java.util.List;

public class ItemMessage extends Message {
    private int index;
    private List<Item> items;
    private List<Category> categories;
    private List<Genre> genres;
    private Book book;
    private Customer customer;
    private int[] itemsIds;
    private String priceOrder;
    private List<Review> reviews;
    private String ratingOrder;
    private double averageRating;
    private String discountOrder;
    private String statusOrder;

    public ItemMessage(String service, String type) {
        super(service, type);
        items = new ArrayList<>();
        categories = new ArrayList<>();
        genres = new ArrayList<>();
        reviews = new ArrayList<>();
    }

    public double getAverageRating() {
        return averageRating;
    }

    public void setAverageRating(double averageRating) {
        this.averageRating = averageRating;
    }

    public List<Genre> getGenres() {
        return genres;
    }

    public void setGenres(List<Genre> genres) {
        this.genres = genres;
    }

    public List<Category> getCategories(){
        return categories;
    }

    public void setCategories(List<Category> categories) {
        this.categories = categories;
    }
    
    public int[] getItemsIds() {
        return itemsIds;
    }
    
    public void setItemsIds(int[] itemsIds) {
        this.itemsIds = itemsIds;
    }

    public int getIndex() {
        return index;
    }

    public void setIndex(int index) {
        this.index = index;
    }

    public List<Item> getItems() {
        return items;
    }

    public void setItems(List<Item> items) {
        this.items = items;
    }

    public Book getBook() {
        return book;
    }

    public void setBook(Book book) {
        this.book = book;
    }

    public Customer getCustomer() {
        return customer;
    }

    public void setCustomer(Customer customer) {
        this.customer = customer;
    }

    public String getPriceOrder() {
        return priceOrder;
    }

    public void setPriceOrder(String priceOrder) {
        this.priceOrder = priceOrder;
    }

    public List<Review> getReviews() {
        return reviews;
    }

    public void setReviews(List<Review> reviews) {
        this.reviews = reviews;
    }

    public String getRatingOrder() {
        return ratingOrder;
    }

    public void setRatingOrder(String ratingOrder) {
        this.ratingOrder = ratingOrder;
    }

    public String getDiscountOrder() {
        return discountOrder;
    }

    public String getStatusOrder() {
        return statusOrder;
    }
}
