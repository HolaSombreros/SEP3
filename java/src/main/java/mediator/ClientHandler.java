package mediator;

import com.google.gson.Gson;
import database.DatabaseManager;
import database.model.Order;

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
                            reply.setOrder(order);
                            sendReply(reply);
         //                   databaseManager.getOrderDAOService().create(order.getItems(), order.getAddress(),order.getDatetime(),order.getOrderStatus(),order.getUser());
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
