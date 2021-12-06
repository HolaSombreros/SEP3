package database.daoservice;

import database.daomodel.CustomerDAO;
import database.daomodel.ItemDAO;
import database.daomodel.ReviewDAO;
import database.daoservice.mapper.ReviewMapper;
import model.Customer;
import model.Item;
import model.MyDateTime;
import model.Review;

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
            e.printStackTrace();
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public Review read(int customer_id, int item_id) {
        try {
            return databaseHelper.mapObject(new ReviewMapper(),"SELECT * FROM review JOIN customer USING(customer_id) JOIN item USING(item_id) WHERE customer_id = ? AND item_id =?", customer_id, item_id);
        }
        catch (SQLException e) {
            e.printStackTrace();
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public List<Review> readByItem(int item_id, int index) {
        try {
            return databaseHelper.mapList(new ReviewMapper(),"SELECT * FROM review JOIN customer USING(customer_id) JOIN item USING(item_id) WHERE item_id =? ORDER BY customer_id ASC LIMIT 3 OFFSET 3 * ? ", item_id, index);
        }
        catch (SQLException e) {
            e.printStackTrace();
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override
    public Review update(Review review) {
        return null;
    }

    @Override
    public Review delete(Review review) {
        return null;
    }
}
