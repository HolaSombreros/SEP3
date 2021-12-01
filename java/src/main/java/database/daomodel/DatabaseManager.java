package database.daomodel;

import database.daoservice.*;

public class DatabaseManager {
    private static String URL = "jdbc:postgresql://localhost:5432/postgres?currentSchema=sep3";
    private static String USERNAME = "sep3admin";
    private static String PASSWORD = "admin";

    private ItemDAO itemDAOService;
    private BookDAO bookDAOService;
    private OrderDAO orderDAOService;
    private CustomerDAO customerDAOService;
    private CategoryDAO categoryDAOService;
    private GenreDAO genreDAOService;
    private AuthorDAO authorDAOService;
    private AddressDAO addressDAOService;
    private NotificationDAO notificationDAOService;

    public DatabaseManager() {
        itemDAOService = new ItemDAOService(URL, USERNAME, PASSWORD);
        bookDAOService = new BookDAOService(URL, USERNAME, PASSWORD);
        orderDAOService = new OrderDAOService(URL, USERNAME, PASSWORD);
        customerDAOService = new CustomerDAOService(URL, USERNAME, PASSWORD);
        categoryDAOService = new CategoryDAOService(URL, USERNAME, PASSWORD);
        genreDAOService = new GenreDAOService(URL, USERNAME, PASSWORD);
        authorDAOService = new AuthorDAOService(URL, USERNAME, PASSWORD);
        addressDAOService = new AddressDAOService(URL, USERNAME, PASSWORD);
        notificationDAOService = new NotificationDAOService(URL, USERNAME, PASSWORD);
    }

    public GenreDAO getGenreDAOService() {
        return genreDAOService;
    }

    public ItemDAO getItemDAOService() {
        return itemDAOService;
    }

    public BookDAO getBookDAOService() {
        return bookDAOService;
    }

    public OrderDAO getOrderDAOService() {
        return orderDAOService;
    }

    public CustomerDAO getCustomerDAOService() {
        return customerDAOService;
    }

    public CategoryDAO getCategoryDAOService() {
        return categoryDAOService;
    }

    public AddressDAO getAddressDAOService() {
        return addressDAOService;
    }

    public AuthorDAO getAuthorDAOService() {
        return getAuthorDAOService();
    }

    public NotificationDAO getNotificationDAOService() {
        return notificationDAOService;
    }
}
