package database.daomodel;

import model.Review;
import java.time.LocalDate;
import java.util.List;

public interface ReviewDAO {
    Review create(int customer_id, int item_id, int rating, String comment, LocalDate dateTime);
    Review read(int customer_id, int item_id);
    List<Review> readByItem(int item_id, int index);
    Review update(Review review);
    Review updateRating(int rating, int item_id, int customer_id);
    void delete(Review review);
}
