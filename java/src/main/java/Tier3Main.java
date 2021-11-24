import database.daomodel.DatabaseManager;
import mediator.Server;
import model.Address;
import model.Category;
import model.Genre;

import java.io.IOException;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;

public class Tier3Main {
    public static void main(String[] args) {
        DatabaseManager databaseManager = new DatabaseManager();
        System.out.println("Database Started  ^-^ ");
        ArrayList<Genre> genres = new ArrayList<>();
        genres.add(new Genre("Romance", 1));
//        databaseManager.getCustomerDAOService().create("Maria","Magdalena","mariamgd@gmail.com","password","Customer", new Address("Old Tavern","Holly Trinity",1234,"Jerusalim",1),"12345678");
//        databaseManager.getBookDAOService()
//            .create("Solo Leveling", "A light Novel", 12.50, new Category(1, "Book"), 15, "Images/solo_leveling_vol1.jpg", "9781975319274", "Chu", "Gong", "English", genres,
//                LocalDate.of(2019, 9, 26));
//        databaseManager.getItemDAOService().create("Ceramic Tea Pot", "a red ceramic tea pot", 50.25, new Category(2, "Home"), 25, "Images/red_ceramic_teapot.png");
//        databaseManager.getBookDAOService()
//            .create("Pride and Prejudice", "A classic novel; Has too many movies", 12.50, new Category(1, "Book"), 25, "Images/pride_and_prejudice.jpg", "9780679783268", "Jane",
//                "Austen", "English", genres, LocalDate.of(1813, 1, 28));
//        databaseManager.getItemDAOService().create("Lavender Candle", "Lavender scented candle", 10.00, new Category(2, "Home"), 25, "Images/lavender_candle.jpg");
//        genres.add(new Genre("Romance", 1));
//        databaseManager.getBookDAOService().create("Solo Leveling", "A light Novel", 12.50, new Category(1,"Book"), 15,"Images/solo_leveling_vol1.jpg","9781975319274","Chu", "Gong",
//              "English",genres, LocalDate.of(2019,9,26));
//        databaseManager.getItemDAOService().create("Ceramic Tea Pot", "a red ceramic tea pot", 50.25,new Category(2, "Home"), 25, "Images/red_ceramic_teapot.png");
//        databaseManager.getBookDAOService().create("Pride and Prejudice", "A classic novel; Has too many movies", 12.50, new Category(1,"Book"), 25,"Images/pride_and_prejudice.jpg","9780679783268","Jane", "Austen",
//                "English", genres, LocalDate.of(1813,1,28));
//        databaseManager.getItemDAOService().create("Lavender Candle", "Lavender scented candle", 10.00,new Category(2, "Home"), 25, "Images/lavender_candle.jpg");
        try {
            Server server = new Server(databaseManager);
            System.out.println("Server started ^-^ ");
            server.run();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
