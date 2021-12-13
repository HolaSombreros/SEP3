package mediator.message;

import model.FAQ;

import java.util.ArrayList;
import java.util.List;

public class FAQMessage extends Message {
    private List<FAQ> faqs;

    public FAQMessage(String service, String type) {
        super(service, type);
        faqs = new ArrayList<>();
    }

    public void setFAQs(List<FAQ> faqs) {
        this.faqs = faqs;
    }

    public List<FAQ> getFaqs() {
        return faqs;
    }
}
