package model;

import java.time.LocalDate;

public class Review {
    private int id;
    private int rating;
    private String comment;
    private Customer customer;
    private int itemId;
    private MyDateTime dateTime;

    public Review(int id,int rating, String comment, Customer customer, int itemId, LocalDate dateTime) {
        this.id = id;
        this.rating = rating;
        this.comment = comment;
        this.itemId = itemId;
        this.customer = customer;
        this.dateTime = new MyDateTime(dateTime.getYear(), dateTime.getMonthValue(), dateTime.getDayOfMonth(), 0, 0, 0);;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public MyDateTime getDateTime() {
        return dateTime;
    }

    public void setDateTime(MyDateTime dateTime) {
        this.dateTime = dateTime;
    }

    public int getRating() {
        return rating;
    }

    public void setRating(int rating) {
        this.rating = rating;
    }

    public String getComment() {
        return comment;
    }

    public void setComment(String comment) {
        this.comment = comment;
    }

    public Customer getCustomer() {
        return customer;
    }

    public void setCustomer(Customer customer) {
        this.customer = customer;
    }

    public int getItemId() {
        return itemId;
    }

    public void setItemId(int itemId) {
        this.itemId = itemId;
    }

    @Override
    public String toString() {
        return "Review{" +
                "rating=" + rating +
                ", comment='" + comment + '\'' +
                '}';
    }

}
