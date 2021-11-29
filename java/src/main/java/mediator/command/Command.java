package mediator.command;

import mediator.request.Request;

public interface Command {
    Request execute(Request request);
}
