package database.daomodel;

import model.FAQ;

import java.util.List;

public interface FAQDAO {
    List<FAQ> readAll();
}
