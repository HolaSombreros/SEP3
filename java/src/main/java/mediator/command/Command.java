package mediator.command;

import mediator.message.Message;

public interface Command {
    Message execute(Message request);
}
