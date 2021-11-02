package via.group2.SEP3.mediator;

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
        //model;
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
                Request request = gson.fromJson(received, Request.class);
                if (request != null) {
                    switch (request.getType()) {
                        case "items":
                            Request reply = new Request("items");
//                            reply.setItems(databaseManager.getItemDAOService().readAll());
                            sendReply(reply);
                            break;
                        case "purchase":
//                            Order order = request.getOrder();;
//                            databaseManager.getOrderDAOService().create(order.getItems(), order.getAddress(),order.getDatetime(),order.getOrderStatus(),order.getUser());
                            break;
                    }
                }
            }
            catch (IOException e) {
                sendReply(new Request("connection_error"));
                running = false;
            }
            catch (Exception e) {
                sendReply(new Request("error"));
            }
        }
    }

    private void sendReply(Request request) {
        String replyGson = gson.toJson(request);
        out.println(replyGson);
    }
}
