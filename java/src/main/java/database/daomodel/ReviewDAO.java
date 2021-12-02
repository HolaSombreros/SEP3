package database.daomodel;

import model.MyDateTime;
import model.Review;

import java.util.List;

public interface ReviewDAO {
    Review create(int customer_id, int item_id, int rating, String comment, MyDateTime dateTime);
    Review read(int customer_id, int item_id);
    List<Review> readByItem(int item_id, int index);
    Review update(Review review);
    Review delete(Review review);
}
