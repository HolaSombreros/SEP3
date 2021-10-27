package database;

import database.daomodel.BookDAO;
import database.daomodel.ItemDAO;
import database.daoservice.BookDAOService;
import database.daoservice.ItemDAOService;

public class DatabaseManager {
    private static String URL = "jdbc:postgresql://localhost:5432/postgres?currentSchema=sep3";
    private static String USERNAME = "sep3admin";
    private static String PASSWORD = "admin";

    private ItemDAO itemDAOService;
    private BookDAO bookDAOService;

    public DatabaseManager() {
        itemDAOService = new ItemDAOService(URL, USERNAME, PASSWORD);
        bookDAOService = new BookDAOService(URL, USERNAME, PASSWORD);
    }

    public ItemDAO getItemDAOService() {
        return itemDAOService;
    }

    public BookDAO getBookDAOService() {
        return bookDAOService;
    }
}
