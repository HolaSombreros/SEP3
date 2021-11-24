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
        methods.put("getAll", this::getAll);
        methods.put("get", this::getItem);
        methods.put("book", this::getBook);
        methods.put("getWishlist", this::getWishlist);
        methods.put("getAllById", this::getAllById);
        methods.put("addWishlist", this::addWishlist);
        methods.put("removeWishlist", this::removeItemFromWishlist);
        methods.put("addShoppingCart", this::addToShoppingCart);
        methods.put("getShoppingCart", this::getShoppingCart);
        methods.put("editShoppingCart", this::updateShoppingCart);
        methods.put("removeShoppingCart", this::removeFromShoppingCart);
        methods.put("searchByName",this::getItemsBySearchName);
        methods.put("getCategories", this::getCategories);
        methods.put("addItem", this::addItem);
        methods.put("getItemBySpecifications", this::getItemBySpecifications);
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

    private void getCategories() {
        reply.setCategories(databaseManager.getCategoryDAOService().readAllCategories());
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

    private void addWishlist() {
        databaseManager.getItemDAOService().addWishlist(request.getCustomer().getId(),request.getItem().getId());
        reply.setItem(databaseManager.getItemDAOService().read(request.getItem().getId()));
    }

    private void removeItemFromWishlist() {
        databaseManager.getItemDAOService().removeItemFromWishlist(request.getCustomer().getId(), request.getItem().getId());
    }
    private void getAllById(){
        reply.setItems(databaseManager.getItemDAOService().readAllByIds(request.getItemsIds()));
    }

    private void addToShoppingCart() {
        databaseManager.getItemDAOService().addToShoppingCart(request.getItem(), request.getCustomer().getId());
        reply.setItem(request.getItem());
    }

    private void getShoppingCart() {
        reply.setItems(databaseManager.getItemDAOService().readShoppingCart(request.getCustomer().getId()));
    }

    private void updateShoppingCart() {
        databaseManager.getItemDAOService().updateShoppingCart(request.getItem(), request.getCustomer().getId());
        reply.setItem(request.getItem());
    }

    private void removeFromShoppingCart() {
        databaseManager.getItemDAOService().removeFromShoppingCart(request.getItem(),request.getCustomer().getId());
    }

    private void getItemsBySearchName(){
        reply.setItems(databaseManager.getItemDAOService().readByItemName(request.getItem().getName(), request.getIndex()));
    }
    private void addItem() {
         reply.setItem(databaseManager.getItemDAOService().create(request.getItem().getName(),request.getItem().getDescription(),
                request.getItem().getPrice(),request.getItem().getCategory(), request.getItem().getQuantity(),request.getItem().getImageName()));
    }
    private void getItemBySpecifications() {
        reply.setItem(databaseManager.getItemDAOService().read(request.getItem().getName(), request.getItem().getDescription(), request.getItem().getCategory()));
    }
}
