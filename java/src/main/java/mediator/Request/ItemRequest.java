package mediator.Request;

import model.Book;
import model.Item;

import java.util.ArrayList;
import java.util.List;

public class ItemRequest extends Request{
    private int index;
    private List<Item> items;
    private Item item;
    private Book book;
    private int customerId;

    public ItemRequest(String service, String type) {
        super(service, type);
        items = new ArrayList<>();
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

    public int getCustomerId() {
        return customerId;
    }

    public void setCustomerId(int customerId) {
        this.customerId = customerId;
    }
}
