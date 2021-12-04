package mediator.message;

import model.FAQ;

import java.util.ArrayList;
import java.util.List;

public class FAQMessage extends Message {
    private List<FAQ> faqs;
    private int id;
    private FAQ faq;

    public FAQMessage(String service, String type) {
        super(service, type);
        faqs = new ArrayList<>();
    }

    public void setFAQs(List<FAQ> faqs) {
        this.faqs = faqs;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public FAQ getFAQ() {
        return faq;
    }

    public void setFAQ(FAQ faq) {
        this.faq = faq;
    }
}
