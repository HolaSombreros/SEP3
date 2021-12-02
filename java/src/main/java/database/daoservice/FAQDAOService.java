package database.daoservice;

import database.daomodel.FAQDAO;
import database.daoservice.mapper.FAQMapper;
import model.FAQ;

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
}
