namespace SimpleRabbitmq;

/// <summary>
/// Event attribute to mark event handlers with event queue name.
/// </summary>
/// <param name="eventName"></param>
[AttributeUsage(AttributeTargets.Class)]
public class EventAttribute(string eventName) : Attribute
{
    /// <summary>
    /// queue name of the event.
    /// </summary>
    public string EventName { get; set; } = eventName;
}
