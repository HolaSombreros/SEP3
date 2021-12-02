package database.daoservice.mapper;

import model.FAQ;

import java.sql.ResultSet;
import java.sql.SQLException;

public class FAQMapper implements DataMapper<FAQ> {
    @Override public FAQ map(ResultSet rs) throws SQLException {
        return new FAQ(rs.getInt("faq_id"),
                rs.getString("category"),
                rs.getString("question"),
                rs.getString("answer")
        );
    }
}
