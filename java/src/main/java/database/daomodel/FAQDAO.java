package database.daomodel;

import model.FAQ;

import java.util.List;

public interface FAQDAO {
    List<FAQ> readAll();
    FAQ read(int id);
    FAQ add(String category, String question, String answer, int creatorId);
    void delete(int id);
}
