package mediator.Request;

import model.Book;
import model.Item;

import java.util.ArrayList;
import java.util.List;

public class ItemRequest extends Request{
    private List<Item> items;
    private Item item;
    private Book book;

    public ItemRequest(String service, String type) {
        super(service, type);
        items = new ArrayList<>();
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
}