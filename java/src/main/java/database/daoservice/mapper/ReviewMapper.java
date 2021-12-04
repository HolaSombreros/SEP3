package database.daoservice.mapper;

import model.Customer;
import model.Review;
import java.sql.ResultSet;
import java.sql.SQLException;

public class ReviewMapper implements DataMapper<Review>{

    @Override
    public Review map(ResultSet resultSet) throws SQLException {
        return new Review(resultSet.getInt("review_id"), resultSet.getInt("rating"),resultSet.getString("comment"),
               new Customer(resultSet.getInt("customer_id"), resultSet.getString("first_name"), resultSet.getString("last_name")),resultSet.getInt("item_id"),resultSet.getDate("date_time").toLocalDate());
    }
}
