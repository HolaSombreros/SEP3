package mediator;

import com.google.gson.Gson;
import database.daomodel.DatabaseManager;
import mediator.command.Command;
import mediator.command.CustomerCommand;
import mediator.command.ItemCommand;
import mediator.command.OrderCommand;
import mediator.message.*;


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
    private Message reply;

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
                Message request = gson.fromJson(received, Message.class);
                if (request != null) {
                    service.get(request.getService()).run();
                    sendReply(command.execute(reply));
                }
            }
            catch (IOException e) {
                ErrorMessage errorRequest = new ErrorMessage("connection_error", "connection_error");
                sendReply(errorRequest);
                running = false;
            }
            catch (Exception e) {
                ErrorMessage errorRequest = new ErrorMessage("error", "error");
                errorRequest.setMessage(e.getMessage());
                sendReply(errorRequest);
            }
        }
    }

    private void itemServiceRun() {
        reply = gson.fromJson(received, ItemMessage.class);
        command = new ItemCommand(databaseManager);
    }

    private void orderServiceRun() {
        reply = gson.fromJson(received, OrderMessage.class);
        command = new OrderCommand(databaseManager);
    }

    private void customerServiceRun() {
        reply = gson.fromJson(received, CustomerMessage.class);
        command = new CustomerCommand(databaseManager);
    }

    private void sendReply(Message reply) {
        String replyGson = gson.toJson(reply);
//        out.println(replyGson.length());
//        System.out.println("----- " + replyGson.length());
//        try {
//            Thread.sleep(500);
//        }
//        catch (InterruptedException e) {
//            e.printStackTrace();
//        }
        out.println(replyGson);
        System.out.println("> " + replyGson);
    }
}
