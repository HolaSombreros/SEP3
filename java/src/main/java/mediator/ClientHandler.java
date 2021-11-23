package mediator;

import com.google.gson.Gson;
import database.daomodel.DatabaseManager;
import mediator.Command.Command;
import mediator.Command.CustomerCommand;
import mediator.Command.ItemCommand;
import mediator.Command.OrderCommand;
import mediator.Request.*;


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
    private HashMap<String, Runnable> service;
    private String received;
    private Request reply;

    public ClientHandler(Socket socket, DatabaseManager databaseManager) throws IOException {
        this.socket = socket;
        in = new BufferedReader(new InputStreamReader(socket.getInputStream()));
        out = new PrintWriter(socket.getOutputStream(), true);
        running = true;
        gson = new Gson();
        this.databaseManager = databaseManager;
        service = new HashMap<>();
        service.put("item", this::itemServiceRun);
        service.put("order", this::orderServiceRun);
        service.put("customer", this::customerServiceRun);
    }

    public void run() {
        while (running) {
            try {
                received = in.readLine();
                System.out.println(received);
                Request request = gson.fromJson(received, Request.class);
                if (request != null) {
                    service.get(request.getService()).run();
                    sendReply(command.execute(reply));
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

    private void itemServiceRun() {
        reply = gson.fromJson(received, ItemRequest.class);
        command = new ItemCommand(databaseManager);
    }

    private void orderServiceRun() {
        reply = gson.fromJson(received, OrderRequest.class);
        command = new OrderCommand(databaseManager);
    }

    private void customerServiceRun() {
        reply = gson.fromJson(received,CustomerRequest.class);
        command = new CustomerCommand(databaseManager);
    }

    private void sendReply(Request reply) {
        String replyGson = gson.toJson(reply);
        out.println(replyGson);
    }
}
