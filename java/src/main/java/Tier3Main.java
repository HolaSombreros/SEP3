import database.daomodel.DatabaseManager;
import mediator.Server;
import model.Author;
import model.Address;
import model.Category;
import model.Genre;

import java.io.IOException;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.Arrays;


public class Tier3Main {
    public static void main(String[] args) {
        DatabaseManager databaseManager = new DatabaseManager();
        System.out.println("Database Started  ^-^ ");
        try {
            Server server = new Server(databaseManager);
            System.out.println("Server started ^-^ ");
            server.run();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
