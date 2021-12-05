package model;

import java.time.LocalDate;

public class Review {
    private int id;
    private int rating;
    private String comment;
    private int customerId;
    private String customerFirstName;
    private String customerLastName;
    private int itemId;
    private MyDateTime dateTime;

    public Review(int id,int rating, String comment, int customerId, int itemId, String customerFirstName, String customerLastName, LocalDate dateTime) {
        this.id = id;
        this.rating = rating;
        this.comment = comment;
        this.itemId = itemId;
        this.customerId = customerId;
        this.customerFirstName =customerFirstName;
        this.customerLastName = customerLastName;
        this.dateTime = new MyDateTime(dateTime.getYear(), dateTime.getMonthValue(), dateTime.getDayOfMonth(), 0, 0, 0);;
    }

    public String getCustomerFirstName() {
        return customerFirstName;
    }

    public void setCustomerFirstName(String customerFirstName) {
        this.customerFirstName = customerFirstName;
    }

    public String getCustomerLastName() {
        return customerLastName;
    }

    public void setCustomerLastName(String customerLastName) {
        this.customerLastName = customerLastName;
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

    public int getCustomerId() {
        return customerId;
    }

    public void setCustomerId(int customerId) {
        this.customerId = customerId;
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
