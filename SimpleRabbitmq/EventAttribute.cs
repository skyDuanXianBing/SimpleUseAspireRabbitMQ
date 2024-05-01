namespace SimpleRabbitmq;

public class EventAttribute(string eventName) : Attribute
{
    public string EventName { get; set; } = eventName;
}
