package via.group2.SEP3.mediator;

import database.DatabaseManager;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;

public class Server implements Runnable {
    public static final int PORT = 1234;
    private boolean running;
    private ServerSocket welcomeSocket;
    private DatabaseManager databaseManager;


    public Server(DatabaseManager databaseManager) throws IOException {
        this.databaseManager = databaseManager;
        running = true;
        welcomeSocket = new ServerSocket(PORT);
    }

    public void close() {
        running = false;
    }

    public void run() {
        while (running) {
            try {
                Socket socket = welcomeSocket.accept();
                ClientHandler clientHandler = new ClientHandler(socket,databaseManager);
                Thread t = new Thread(clientHandler);
                t.start();
            }
            catch (IOException e) {
                close();
            }
        }
    }
}
