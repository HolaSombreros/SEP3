import database.daomodel.DatabaseManager;
import mediator.Server;

import java.io.IOException;

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
