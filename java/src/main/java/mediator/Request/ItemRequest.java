package mediator.Request;

import model.Book;
import model.Customer;
import model.Item;

import java.util.ArrayList;
import java.util.List;

public class ItemRequest extends Request{
    private int index;
    private List<Item> items;
    private Item item;
    private Book book;
    private Customer customer;
    private int[] itemsIds;
    private int customerId;

    public ItemRequest(String service, String type) {
        super(service, type);
        items = new ArrayList<>();
    }
    
    public int[] getItemsIds() {
        return itemsIds;
    }
    
    public void setItemsIds(int[] itemsIds) {
        this.itemsIds = itemsIds;
    }

    public int getIndex() {
        return index;
    }

    public void setIndex(int index) {
        this.index = index;
    }

    public List<Item> getItems() {
        return items;
    }

    public void setItems(List<Item> items) {
        this.items = items;
    }

    public Item getItem() {
        return item;
    }

    public void setItem(Item item) {
        this.item = item;
    }

    public Book getBook() {
        return book;
    }

    public void setBook(Book book) {
        this.book = book;
    }

    public Customer getCustomer() {
        return customer;
    }

    public void setCustomer(Customer customer) {
        this.customer = customer;
    }
}
