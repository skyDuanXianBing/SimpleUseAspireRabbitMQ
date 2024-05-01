namespace SimpleRabbitmq;

public interface IEventBus
{
    Task Publish(string eventName, object message);
    //void Publich<T>(string eventName,T message);
     Task Subscribe(string eventName, Type handlerType);
}
