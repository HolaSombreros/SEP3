package mediator;

import com.google.gson.Gson;
import database.daomodel.DatabaseManager;
import model.Item;
import model.Order;
import model.enums.Category;


import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.Socket;

public class ClientHandler implements Runnable {


    private Socket socket;
    private BufferedReader in;
    private PrintWriter out;
    private boolean running;
    private Gson gson;
    private DatabaseManager databaseManager;

    public ClientHandler(Socket socket, DatabaseManager databaseManager) throws IOException {
        this.socket = socket;
        in = new BufferedReader(new InputStreamReader(socket.getInputStream()));
        out = new PrintWriter(socket.getOutputStream(), true);
        running = true;
        gson = new Gson();
        this.databaseManager = databaseManager;
    }

    public void run() {
        while (running) {
            try {
                String received = in.readLine();
                System.out.println(received);
                Request request = gson.fromJson(received, Request.class);
                Request reply;
                if (request != null) {
                    switch (request.getType()) {
                        case "items":
                            reply = new Request(request.getType());
                            reply.setItems(databaseManager.getItemDAOService().readAll());
                            sendReply(reply);
                            break;
                        case "purchase":
                            reply = new Request(request.getType());
                            Order order = request.getOrder();
                            System.out.println(order.getAddress().toString());
                            reply.setOrder(databaseManager.getOrderDAOService().create(order.getItems(), order.getAddress(),order.getDateTime(),order.getOrderStatus(),order.getFirstName(), order.getLastName(), order.getEmail()));
                            sendReply(reply);
                            break;
                        case "item":
                            reply = new Request(request.getType());
                            Item item = databaseManager.getItemDAOService().read(request.getItem().getId());
                            if(item.getCategory() == Category.BOOK)
                                reply.setItem(databaseManager.getBookDAOService().read(request.getItem().getId()));
                            else
                                reply.setItem(databaseManager.getItemDAOService().read(request.getItem().getId()));
                            sendReply(reply);
                            break;
                    }
                }
            }
            catch (IOException e) {
                e.printStackTrace();
                sendReply(new Request("connection_error"));
                running = false;
            }
            catch (Exception e) {
                e.printStackTrace();
                sendReply(new Request("error"));
            }
        }
    }

    private void sendReply(Request reply) {
        String replyGson = gson.toJson(reply);
        System.out.println(replyGson);
        out.println(replyGson);
    }
}
