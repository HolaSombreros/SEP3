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
        methods.put("getItem", this::getItem);
        methods.put("getBook", this::getBook);
        methods.put("getWishlist", this::getWishlist);
        methods.put("getAllById", this::getAllById);
        methods.put("addWishlist", this::addWishlist);
        methods.put("removeWishlist", this::removeItemFromWishlist);
        methods.put("getCategories", this::getCategories);
        methods.put("deleteCategory", this::deleteCategory);
        methods.put("getGenres", this::getGenres);
        methods.put("addItem", this::addItem);
        methods.put("addBook",this::addBook);
        methods.put("getItemBySpecifications", this::getItemBySpecifications);
        methods.put("getBookBySpecifications", this::getBookBySpecifications);
        methods.put("addShoppingCart", this::addToShoppingCart);
        methods.put("getShoppingCart", this::getShoppingCart);
        methods.put("updateShoppingCart", this::updateShoppingCart);
        methods.put("removeShoppingCart", this::removeFromShoppingCart);
        methods.put("addCategory", this::addCategory);
        methods.put("updateItem", this::updateItem);
        methods.put("updateBook", this::updateBook);
        methods.put("getItemReviews", this::getItemReviews);
        methods.put("addReview", this::addReview);
        methods.put("removeReview", this::removeReview);
        methods.put("getReview", this::getReview);
        methods.put("updateReview", this::updateReview);
        methods.put("getAverageRating", this::getAverageRating);
    }

    /**
     * Executes the request based on the type attribute, calls the method tied to the type attribute
     * Returns the reply that was modified by the called method
     * @param request
     * @return
     */
    @Override public Message execute(Message request) {
        try {
            this.request = (ItemMessage) request;
            reply = new ItemMessage(request.getService(), request.getType());
            methods.get(request.getType()).run();
            return reply;
        }
        catch (Exception e) {
            throw new IllegalArgumentException("The request could not be fulfilled");
        }
    }

    private void getAll() {
        reply.setItems(databaseManager.getItemDAOService().readByIndex(request.getIndex(), request.getCategories().get(0).getName(),request.getPriceOrder(), request.getRatingOrder(), request.getDiscountOrder(),
                request.getStatusOrder(), request.getItems().get(0).getName()));
    }

    private void getCategories() {
        reply.setCategories(databaseManager.getCategoryDAOService().readAllCategories());
    }

    private void getGenres() {
        reply.setGenres(databaseManager.getGenreDAOService().getAllGenres());
    }

    private void getItem() {
        reply.getItems().add(0,databaseManager.getItemDAOService().read(request.getItems().get(0).getId()));
    }

    private void getBook() {
        reply.setBook(databaseManager.getBookDAOService().read(request.getItems().get(0).getId()));
    }

    private void getWishlist() {
        reply.setItems(databaseManager.getItemDAOService().readCustomerWishlist(request.getCustomer().getId()));
    }

    private void addWishlist() {
        databaseManager.getItemDAOService().addWishlist(request.getCustomer().getId(),request.getItems().get(0).getId());
        reply.getItems().add(0,databaseManager.getItemDAOService().read(request.getItems().get(0).getId()));
    }

    private void removeItemFromWishlist() {
        databaseManager.getItemDAOService().removeItemFromWishlist(request.getCustomer().getId(), request.getItems().get(0).getId());
    }
    
    private void getAllById(){
        reply.setItems(databaseManager.getItemDAOService().readAllByIds(request.getItemsIds()));
    }

    private void addItem() {
         reply.getItems().add(0,databaseManager.getItemDAOService().create(request.getItems().get(0).getName(),request.getItems().get(0).getDescription(),
                request.getItems().get(0).getPrice(),request.getItems().get(0).getDiscount(),request.getItems().get(0).getCategory(), request.getItems().get(0).getQuantity(),request.getItems().get(0).getImageName()));
    }

    private void updateItem(){
        reply.getItems().add(0,databaseManager.getItemDAOService().update(request.getItems().get(0)));
    }

    private void addBook(){
        reply.setBook(databaseManager.getBookDAOService().create(request.getBook().getName(), request.getBook().getDescription(),request.getBook().getPrice(),request.getBook().getDiscount(), request.getBook().getCategory(),
                request.getBook().getQuantity(),request.getBook().getImageName(),request.getBook().getISBN(), request.getBook().getAuthors(),request.getBook().getLanguage(),request.getBook().getGenre(),
                LocalDate.of(request.getBook().getPublicationDate().getYear(), request.getBook().getPublicationDate().getMonth(),request.getBook().getPublicationDate().getDay())));
    }

    private void updateBook(){
        reply.setBook(databaseManager.getBookDAOService().update(request.getBook()));
    }

    private void getItemBySpecifications() {
        reply.getItems().add(0,databaseManager.getItemDAOService().read(request.getItems().get(0).getName(), request.getItems().get(0).getDescription(), request.getItems().get(0).getCategory()));
    }

    private void getBookBySpecifications() {
        reply.setBook(databaseManager.getBookDAOService().read(request.getBook().getISBN()));
    }


    private void addToShoppingCart() {
        databaseManager.getItemDAOService().addToShoppingCart(request.getItems().get(0), request.getCustomer().getId());
        reply.getItems().add(0,request.getItems().get(0));
    }

    private void getShoppingCart() {
        reply.setItems(databaseManager.getItemDAOService().readShoppingCart(request.getCustomer().getId()));
    }

    private void updateShoppingCart() {
        databaseManager.getItemDAOService().updateShoppingCart(request.getItems().get(0), request.getCustomer().getId());
        reply.getItems().add(0,request.getItems().get(0));
    }

    private void removeFromShoppingCart() {
        databaseManager.getItemDAOService().removeFromShoppingCart(request.getItems().get(0),request.getCustomer().getId());
    }


    private void addCategory() {
        List<Category> categories = new ArrayList<>();
        categories.add(databaseManager.getCategoryDAOService().createCategory(request.getCategories().get(0).getName()));
        reply.setCategories(categories);
    }

    private void deleteCategory() {
        databaseManager.getCategoryDAOService().delete(request.getCategories().get(0).getId());
    }

    private void getItemReviews() {
        reply.setReviews(databaseManager.getReviewDAOService().readByItem(request.getItems().get(0).getId(), request.getIndex()));
    }

    private void addReview() {
        Review review = request.getReviews().get(0);
        reply.getReviews().add(0,databaseManager.getReviewDAOService().create(review.getCustomer().getId(), review.getItemId(),review.getRating(),
                review.getComment(), LocalDate.of(review.getDateTime().getYear(), review.getDateTime().getMonth(), review.getDateTime().getDay())));
    }

    private void removeReview() {
        databaseManager.getReviewDAOService().delete(request.getReviews().get(0));
    }

    private void getReview() {
        reply.getReviews().add(0,databaseManager.getReviewDAOService().read(request.getReviews().get(0).getCustomer().getId(), request.getReviews().get(0).getItemId()));
    }

    private void updateReview() {
        reply.getReviews().add(0,databaseManager.getReviewDAOService().update(request.getReviews().get(0)));
    }

    private void getAverageRating(){
        reply.setAverageRating(databaseManager.getReviewDAOService().getAverageRatingForItem(request.getItems().get(0).getId()));
    }

}
