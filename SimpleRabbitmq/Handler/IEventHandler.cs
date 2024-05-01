namespace SimpleRabbitmq.Handler;

public interface IEventHandler
{
    Task Handle(string eventName, object eventData);
}
