package database.daoservice;

import database.daomodel.CustomerDAO;
import database.daomodel.ItemDAO;
import database.daomodel.ReviewDAO;
import database.daoservice.mapper.ReviewMapper;
import model.Customer;
import model.Item;
import model.Review;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.time.LocalDate;
import java.util.List;

public class ReviewDAOService implements ReviewDAO {

    private DatabaseHelper<Review> databaseHelper;
    private ItemDAO itemDAOService;
    private CustomerDAO customerDAOService;

    public ReviewDAOService(String url, String username, String password) {
        this.databaseHelper = new DatabaseHelper<>(url, username, password);
        itemDAOService = new ItemDAOService(url, username, password);
        customerDAOService = new CustomerDAOService(url, username, password);
    }
    @Override
    public Review create(int customer_id, int item_id, int rating, String comment, LocalDate dateTime) {
        try {
            Item item = itemDAOService.read(item_id);
            Customer customer = customerDAOService.read(customer_id);
            if(customer == null)
                throw new IllegalArgumentException("The customer does not exist");
            else if(item == null)
                throw new IllegalArgumentException("The item does not exist");
            else {
                databaseHelper.executeUpdate("INSERT INTO review(rating, comment, customer_id, item_id, date_time) VALUES (?,?,?,?,?)", rating,comment,customer_id,item_id,dateTime);
                return read(customer_id, item_id);
            }
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public Review read(int customer_id, int item_id) {
        try {
            return databaseHelper.mapObject(new ReviewMapper(),"SELECT * FROM review JOIN customer USING(customer_id) JOIN item USING(item_id) WHERE customer_id = ? AND item_id =?", customer_id, item_id);
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public List<Review> readByItem(int item_id, int index) {
        try {
            return databaseHelper.mapList(new ReviewMapper(),"SELECT * FROM review JOIN customer USING(customer_id) JOIN item USING(item_id) WHERE item_id = ? ORDER BY customer_id DESC LIMIT 3 OFFSET 3 * ? ", item_id, index);
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public Review update(Review review) {
        try {
            databaseHelper.executeUpdate("UPDATE review SET rating = ?, comment = ?, date_time = ? WHERE customer_id =? AND item_id = ?",review.getRating(),review.getComment(),
                    LocalDate.of(review.getDateTime().getYear(), review.getDateTime().getMonth(), review.getDateTime().getDay()),review.getCustomer().getId(), review.getItemId());
            return read(review.getCustomer().getId(), review.getItemId());
        }
        catch (SQLException e){
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public void delete(Review review) {
        try {
            databaseHelper.executeUpdate("DELETE FROM review WHERE item_id = ? AND customer_id = ?;", review.getItemId(), review.getCustomer().getId());
        }
        catch(SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public double getAverageRatingForItem(int itemId) {
        try{
            ResultSet resultSet = databaseHelper.executeQuery(databaseHelper.getConnection(), "SELECT COALESCE(AVG(rating),0) AS avg_rating FROM review WHERE item_id = ?",itemId);
            if(resultSet.next()){
                return resultSet.getDouble("avg_rating");
            }
            return 0;

        }catch(SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }
}
