namespace SimpleRabbitmq;

public interface IEventBus
{
    /// <summary>
    /// Publish a message to the specified event name.
    /// </summary>
    /// <param name="eventName">queue name</param>
    /// <param name="message">message object</param>
    /// <returns></returns>
    Task Publish(string eventName, object message);
    
    /// <summary>
    /// Subscribe to a specific event name with a handler type.
    /// </summary>
    /// <param name="eventName">queue name</param>
    /// <param name="handlerType">handler type</param>
    /// <returns></returns>
     Task Subscribe(string eventName, Type handlerType);
}
