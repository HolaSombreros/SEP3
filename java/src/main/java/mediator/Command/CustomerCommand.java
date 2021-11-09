package mediator.Command;

import database.daomodel.DatabaseManager;
import mediator.Request.CustomerRequest;
import mediator.Request.Request;

public class CustomerCommand implements Command {

    private DatabaseManager databaseManager;

    public CustomerCommand(DatabaseManager databaseManager) {
        this.databaseManager = databaseManager;
    }

    @Override public Request execute(Request request) {
        CustomerRequest reply;
        switch (request.getType()) {
            case "login":
            case "register":
        }
        throw new IllegalArgumentException("The request could not be fulfilled");
    }
}
