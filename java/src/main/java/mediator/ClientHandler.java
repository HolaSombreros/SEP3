package mediator;

import com.google.gson.Gson;
import database.DatabaseManager;
import database.model.Item;
import database.model.enums.Category;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.Socket;
import java.util.ArrayList;

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
                            ArrayList<Item> items = new ArrayList<>();
                            items.add(new Item(1,"name", "description", 100, Category.BOOK, 13));
                            reply.setItems(items);
                            sendReply(reply);
                            break;
                        case "purchase":
                            //model.placeOrder
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
