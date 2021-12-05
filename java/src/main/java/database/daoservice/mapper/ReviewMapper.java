package database.daoservice.mapper;

import model.Review;
import java.sql.ResultSet;
import java.sql.SQLException;

public class ReviewMapper implements DataMapper<Review>{

    @Override
    public Review map(ResultSet resultSet) throws SQLException {
        return new Review(resultSet.getInt("review_id"), resultSet.getInt("rating"),resultSet.getString("comment"),
                resultSet.getInt("customer_id"),resultSet.getInt("item_id"),resultSet.getString("first_name"), resultSet.getString("last_name"),resultSet.getDate("date_time").toLocalDate());
    }
}

