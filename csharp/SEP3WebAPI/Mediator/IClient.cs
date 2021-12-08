using SEP3WebAPI.Mediator.Messages;

namespace SEP3WebAPI.Mediator {
    public interface IClient {
        Message Send(object req);
    }
}