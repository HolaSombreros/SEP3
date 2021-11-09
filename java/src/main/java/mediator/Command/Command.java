package mediator.Command;

import mediator.Request.Request;

public interface Command {
    Request execute(Request request);
}
