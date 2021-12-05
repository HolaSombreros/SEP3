package mediator.command;
import database.daomodel.DatabaseManager;
import mediator.message.ItemMessage;
import mediator.message.Message;
import model.Category;
import model.Review;

import java.time.LocalDate;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

public class ItemCommand implements Command {

    private ItemMessage request;
    private ItemMessage reply;
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
        methods.put("getCategories", this::getCategories);
        methods.put("getGenres", this::getGenres);
        methods.put("addItem", this::addItem);
        methods.put("addBook",this::addBook);
        methods.put("getItemBySpecifications", this::getItemBySpecifications);
        methods.put("getBookBySpecifications", this::getBookBySpecifications);
        methods.put("addShoppingCart", this::addToShoppingCart);
        methods.put("getShoppingCart", this::getShoppingCart);
        methods.put("editShoppingCart", this::updateShoppingCart);
        methods.put("removeShoppingCart", this::removeFromShoppingCart);
        methods.put("searchByName",this::getItemsBySearchName);
        methods.put("getAllByCategory", this::getAllByCategory);
        methods.put("addCategory", this::addCategory);
        methods.put("updateItem", this::updateItem);
        methods.put("updateBook", this::updateBook);
        methods.put("getAllByPrice",this::getALlByPrice);
        methods.put("getItemReviews", this::getItemReviews);
        methods.put("addReview", this::addReview);
    }

    @Override public Message execute(Message request) {
        try {
            this.request = (ItemMessage) request;
            reply = new ItemMessage(request.getService(), request.getType());
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

    private void getGenres() {
        reply.setGenres(databaseManager.getGenreDAOService().getAllGenres());
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

    private void addItem() {
         reply.setItem(databaseManager.getItemDAOService().create(request.getItem().getName(),request.getItem().getDescription(),
                request.getItem().getPrice(),request.getItem().getCategory(), request.getItem().getQuantity(),request.getItem().getImageName()));
    }

    private void updateItem(){
        reply.setItem(databaseManager.getItemDAOService().update(request.getItem()));
    }

    private void addBook(){
        reply.setBook(databaseManager.getBookDAOService().create(request.getBook().getName(), request.getBook().getDescription(),request.getBook().getPrice(),request.getBook().getCategory(),
                request.getBook().getQuantity(),request.getBook().getImageName(),request.getBook().getISBN(), request.getBook().getAuthors(),request.getBook().getLanguage(),request.getBook().getGenre(),
                LocalDate.of(request.getBook().getPublicationDate().getYear(), request.getBook().getPublicationDate().getMonth(),request.getBook().getPublicationDate().getDay())));
    }

    private void updateBook(){
        reply.setBook(databaseManager.getBookDAOService().update(request.getBook()));
    }

    private void getItemBySpecifications() {
        reply.setItem(databaseManager.getItemDAOService().read(request.getItem().getName(), request.getItem().getDescription(), request.getItem().getCategory()));
    }

    private void getBookBySpecifications() {
        reply.setBook(databaseManager.getBookDAOService().read(request.getBook().getISBN()));
    }
    
    private void getItemsBySearchName(){
        reply.setItems(databaseManager.getItemDAOService().readByItemName(request.getItem().getName(), request.getIndex()));
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

    private void getAllByCategory() {
        reply.setItems(databaseManager.getItemDAOService().readAllByCategory(request.getItem().getName(), request.getIndex()));
    }

    private void addCategory() {
        List<Category> categories = new ArrayList<>();
        categories.add(databaseManager.getCategoryDAOService().createCategory(request.getCategories().get(0).getName()));
        reply.setCategories(categories);
    }

    private void getALlByPrice(){
        reply.setItems(databaseManager.getItemDAOService().readAllByPrice(request.getOrderBy(), request.getIndex()));
    }

    private void getItemReviews() {
        reply.setReviews(databaseManager.getReviewDAOService().readByItem(request.getItem().getId(), request.getIndex()));
    }

    private void addReview() {
        Review review = request.getReviews().get(0);
        reply.getReviews().add(databaseManager.getReviewDAOService().create(review.getCustomerId(), review.getItemId(),review.getRating(),
                review.getComment(), LocalDate.of(review.getDateTime().getYear(), review.getDateTime().getMonth(), review.getDateTime().getDay())));
    }
}
