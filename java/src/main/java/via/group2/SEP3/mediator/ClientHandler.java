package via.group2.SEP3.mediator;

import com.google.gson.Gson;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.Socket;

public class ClientHandler implements Runnable {

    // MODEL
    private Socket socket;
    private BufferedReader in;
    private PrintWriter out;
    private boolean running;
    private Gson gson;

    public ClientHandler(Socket socket /*model*/) throws IOException {
        //model;
        this.socket = socket;
        in = new BufferedReader(new InputStreamReader(socket.getInputStream()));
        out = new PrintWriter(socket.getOutputStream(), true);
        running = true;
        gson = new Gson();
    }

    public void run() {
        while (running) {
            try {
                String request = in.readLine();
                // TODO

            }
            catch (IOException e) {
                running = false;
            }
        }
    }
}
