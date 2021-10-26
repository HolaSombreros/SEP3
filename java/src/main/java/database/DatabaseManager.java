package database;

import database.daoservice.BookDAOService;
import database.daoservice.ItemDAOService;

public class DatabaseManager {
    private static String URL = "jdbc:postgresql://localhost:5432/postgres?currentSchema=sep3";
    private static String USERNAME = "sep3admin";
    private static String PASSWORD = "admin";

    private ItemDAOService itemDAOService;
    private BookDAOService bookDAOService;

    public DatabaseManager() {
        itemDAOService = new ItemDAOService(URL, USERNAME, PASSWORD);
        bookDAOService = new BookDAOService(URL, USERNAME, PASSWORD);
    }

    public ItemDAOService getItemDAOService() {
        return itemDAOService;
    }

    public BookDAOService getBookDAOService() {
        return bookDAOService;
    }
}
