package mediator;

import com.google.gson.Gson;
import database.daomodel.DatabaseManager;
import mediator.Command.Command;
import mediator.Command.CustomerCommand;
import mediator.Command.ItemCommand;
import mediator.Command.OrderCommand;
import mediator.Request.*;
import model.Item;
import model.Order;
import model.enums.Category;


import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.Socket;
import java.util.HashMap;

public class ClientHandler implements Runnable {


    private Socket socket;
    private BufferedReader in;
    private PrintWriter out;
    private Command command;
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
                Request request = gson.fromJson(received, Request.class);
                if (request != null) {
                    switch (request.getService()) {
                        case "item":
                            ItemRequest itemRequest = gson.fromJson(received, ItemRequest.class);
                            command = new ItemCommand(databaseManager);
                            sendReply(command.execute(itemRequest));
                            break;
                        case "order":
                            OrderRequest orderRequest = gson.fromJson(received, OrderRequest.class);
                            command = new OrderCommand(databaseManager);
                            sendReply(command.execute(orderRequest));
                            break;
                        case "customer":
                            CustomerRequest customerRequest = gson.fromJson(received, CustomerRequest.class);
                            command = new CustomerCommand(databaseManager);
                            sendReply(command.execute(customerRequest));
                            break;
                    }
                }
            }
            catch (IOException e) {
                ErrorRequest errorRequest = new ErrorRequest("connection_error", "connection_error");
                sendReply(errorRequest);
                running = false;
            }
            catch (Exception e) {
                ErrorRequest errorRequest = new ErrorRequest("error", "error");
                errorRequest.setMessage(e.getMessage());
                sendReply(errorRequest);
            }
        }
    }

    private void sendReply(Request reply) {
        String replyGson = gson.toJson(reply);
        out.println(replyGson);
    }
}
