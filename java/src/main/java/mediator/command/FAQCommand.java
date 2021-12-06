package mediator.command;

import database.daomodel.DatabaseManager;
import mediator.message.FAQMessage;
import mediator.message.Message;
import model.FAQ;

import java.util.HashMap;
import java.util.List;

public class FAQCommand implements Command {
    private FAQMessage request;
    private FAQMessage reply;
    private DatabaseManager databaseManager;
    private HashMap<String, Runnable> methods;

    public FAQCommand(DatabaseManager databaseManager) {
        this.databaseManager = databaseManager;
        methods = new HashMap<>();
        methods.put("getAll", this::getAll);
        methods.put("get", this::get);
        methods.put("add", this::add);
        methods.put("delete", this::delete);
    }

    @Override public Message execute(Message request) {
        try {
            this.request = (FAQMessage) request;
            reply = new FAQMessage(request.getService(), request.getType());
            methods.get(request.getType()).run();
            return reply;
        } catch (Exception e) {
            e.printStackTrace();
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    private void getAll() {
        List<FAQ> faqs = databaseManager.getFAQDAOService().readAll();
        reply.setFAQs(faqs);
    }

    private void get() {
        FAQ faq = databaseManager.getFAQDAOService().read(request.getId());
        reply.setFAQ(faq);
    }

    private void add() {
        FAQ faq = request.getFAQ();
        FAQ created = databaseManager.getFAQDAOService().add(faq.getCategory(), faq.getQuestion(), faq.getAnswer(), faq.getCreatorId());
        reply.setFAQ(created);
    }

    private void delete() {
        databaseManager.getFAQDAOService().delete(request.getId());
    }
}
