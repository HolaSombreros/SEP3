import database.daomodel.DatabaseManager;
import mediator.Server;
import model.enums.Category;
import model.enums.Genre;
import model.enums.Language;

import java.io.IOException;
import java.time.LocalDate;

public class Tier3Main {
    public static void main(String[] args) {
        DatabaseManager databaseManager = new DatabaseManager();
        System.out.println("Database Started  ^-^ ");
        databaseManager.getBookDAOService().create("Solo Leveling", "A light Novel", 12.50, Category.BOOK, 15,"Images/solo_leveling_vol1.jpg","9781975319274","Chu", "Gong",
              Language.ENGLISH, Genre.LIGHTNOVEL, LocalDate.of(2019,9,26));
        databaseManager.getItemDAOService().create("Ceramic Tea Pot", "a red ceramic tea pot", 50.25,Category.HOME, 25, "Images/red_ceramic_teapot.png");
        databaseManager.getBookDAOService().create("Pride and Prejudice", "A classic novel; Has too many movies", 12.50, Category.BOOK, 25,"Images/pride_and_prejudice.jpg","9780679783268","Jane", "Austen",
                Language.ENGLISH, Genre.ROMANCE, LocalDate.of(1813,1,28));
        databaseManager.getItemDAOService().create("Lavender Candle", "Lavender scented candle", 10.00,Category.HOME, 25, "Images/lavender_candle.jpg");

        try {
            Server server = new Server(databaseManager);
            System.out.println("Server started ^-^ ");
            server.run();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
