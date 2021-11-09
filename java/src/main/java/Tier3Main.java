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
   //     databaseManager.getBookDAOService().create("Solo Leveling", "A light Novel", 12.50, Category.BOOK, 15,"Images/solo_leveling_vol1.jpg","9781975319274","Chu", "Gong",
     //       Language.ENGLISH, Genre.LIGHTNOVEL, LocalDate.of(2019,9,26));
       // databaseManager.getItemDAOService().create("Ceramic Tea Pot", "a red ceramic tea pot", 50.25,Category.HOME, 25, "Images/red_ceramic_teapot.png");
        try {
            Server server = new Server(databaseManager);
            System.out.println("Server started ^-^ ");
            server.run();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
