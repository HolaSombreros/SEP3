package mediator.Command;

import database.daomodel.DatabaseManager;
import mediator.Request.ItemRequest;
import mediator.Request.Request;
import model.Item;

import java.util.HashMap;

public class ItemCommand implements Command {

    private ItemRequest request;
    private ItemRequest reply;
    private DatabaseManager databaseManager;
    private HashMap<String, Runnable> methods;

    public ItemCommand(DatabaseManager databaseManager) {
        this.databaseManager = databaseManager;
        methods = new HashMap<>();
        methods.put("getAll",this::getAll);
        methods.put("get",this::getItem);
        methods.put("book",this::getBook);
        methods.put("getWishlist", this::getWishlist);
        methods.put("removeWishlist", this::removeItemFromWishlist);
    }

    @Override public Request execute(Request request) {
        try {
            this.request = (ItemRequest) request;
            reply = new ItemRequest(request.getService(), request.getType());
            methods.get(request.getType()).run();
            return reply;
        }
        catch (Exception e) {
            e.printStackTrace();
            throw new IllegalArgumentException("The request could not be fulfilled");
        }
    }

    private void getAll() {
        reply.setItems(databaseManager.getItemDAOService().readByIndex(request.getIndex()));
    }

    private void getItem() {
        reply.setItem(databaseManager.getItemDAOService().read(request.getItem().getId()));
    }

    private void getBook() {
        reply.setBook(databaseManager.getBookDAOService().read(request.getItem().getId()));
    }

    private void getWishlist() {
        reply.setItems(databaseManager.getItemDAOService().readCustomerWishlist(request.getCustomer().getId()));
    }

    private void removeItemFromWishlist() {
        databaseManager.getItemDAOService().removeItemFromWishlist(request.getCustomer().getId(), request.getItem().getId());
    }
}
