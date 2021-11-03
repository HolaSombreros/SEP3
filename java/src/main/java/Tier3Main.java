import database.DatabaseManager;
import database.model.enums.Category;
import database.model.enums.Genre;
import database.model.enums.Language;
import mediator.Server;

import java.io.IOException;
import java.time.LocalDate;

public class Tier3Main {
    public static void main(String[] args) {
        DatabaseManager databaseManager = new DatabaseManager();
//        databaseManager.getBookDAOService().create("Solo Leveling", "A light Novel", 12.50, Category.BOOK, 15,"9781975319274","Chu", "Gong",
//              Language.ENGLISH, Genre.LIGHTNOVEL, LocalDate.of(2019,9,26));
//        databaseManager.getItemDAOService().create("Ceramic Tea Pot", "a red ceramic tea pot", 50.25,Category.HOME, 25);

        try {
            Server server = new Server(databaseManager);
            server.run();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
