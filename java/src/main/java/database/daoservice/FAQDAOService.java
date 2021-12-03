package database.daoservice;

import database.daomodel.FAQDAO;
import database.daoservice.mapper.FAQMapper;
import model.FAQ;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.List;

public class FAQDAOService implements FAQDAO {
    private DatabaseHelper<FAQ> databaseHelper;

    public FAQDAOService(String url, String username, String password) {
        databaseHelper = new DatabaseHelper<>(url, username, password);
    }

    @Override public List<FAQ> readAll() {
        try {
            return databaseHelper.mapList(new FAQMapper(), "SELECT * FROM faq JOIN faq_category USING (category_id) ORDER BY category ASC;");
        }
        catch (SQLException e) {
            throw new IllegalStateException(e.getMessage());
        }
    }

    @Override public FAQ read(int id) {
        try {
            return databaseHelper.mapObject(new FAQMapper(), "SELECT * FROM faq JOIN faq_category USING (category_id) WHERE faq_id = ?;", id);
        } catch (SQLException e) {
            throw new IllegalStateException(e.getMessage());
        }
    }

    @Override public FAQ add(String category, String question, String answer) {
        try {
            int categoryId;
            ResultSet rs = databaseHelper.executeQuery(databaseHelper.getConnection(), "SELECT * FROM faq_category "
                    + "WHERE lower(category) = lower(?);", category);

            if (!rs.next()) {
                List<Integer> categoryKeys = databaseHelper
                        .executeUpdateWithKeys("INSERT INTO faq_category (category) VALUES (?);", category);
                categoryId = categoryKeys.get(0);
            } else {
                categoryId = rs.getInt("category_id");
            }

            List<Integer> faqKeys = databaseHelper.executeUpdateWithKeys("INSERT INTO faq (category_id, question, answer) "
                    + "VALUES (?, ?, ?);", categoryId, question, answer);
            return new FAQ(faqKeys.get(0), category, question, answer);
        } catch (SQLException e) {
            throw new IllegalStateException(e.getMessage());
        }
    }

    @Override public void delete(int id) {
        try {
            databaseHelper.executeUpdate("DELETE FROM faq WHERE faq_id = ?;", id);
        } catch (SQLException e) {
            throw new IllegalStateException(e.getMessage());
        }
    }
}
