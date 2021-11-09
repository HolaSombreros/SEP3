package mediator.Command;

import database.daomodel.DatabaseManager;
import mediator.Request.ItemRequest;
import mediator.Request.Request;
import model.Item;
import model.enums.Category;

public class ItemCommand implements Command {

    private DatabaseManager databaseManager;

    public ItemCommand(DatabaseManager databaseManager) {
        this.databaseManager = databaseManager;
    }

    @Override public Request execute(Request request) {
        ItemRequest reply = new ItemRequest(request.getService(), request.getType());;
        switch (request.getType()) {
            case "getAll":
                reply.setItems(databaseManager.getItemDAOService().readAll());
                return reply;
            case "get":
                Item item = databaseManager.getItemDAOService().read(((ItemRequest)request).getItem().getId());
                if (item.getCategory() == Category.BOOK)
                    reply.setBook(databaseManager.getBookDAOService().read(((ItemRequest) request).getItem().getId()));
                else
                    reply.setItem(databaseManager.getItemDAOService().read(((ItemRequest) request).getItem().getId()));
                return reply;
        }
        throw new IllegalArgumentException("The request could not be fulfilled");
    }
}
