namespace SimpleRabbitmq.Handler;

public interface IEventHandler
{
    /// <summary>
    /// Handle the event
    /// </summary>
    /// <param name="eventName">queue name</param>
    /// <param name="eventData">message content</param>
    /// <returns></returns>
    Task Handle(string eventName, object eventData);
}
