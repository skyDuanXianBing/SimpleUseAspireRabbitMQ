using System.Text.Json;

namespace SimpleRabbitmq.Handler;

/// <summary>
/// Interface for handling events in JSON format.
/// </summary>
/// <typeparam name="EventT"></typeparam>
public abstract class IEventJsonHandler<EventT> : IEventHandler
{
    public Task Handle(string eventName, object eventData)
    {
        ArgumentNullException.ThrowIfNull(eventData);
        var Data = JsonSerializer.Deserialize<EventT>(eventData.ToString());
        return HandleJson(eventName, Data);
    }

    /// <summary>
    /// Handle the event in JSON format.
    /// </summary>
    /// <param name="eventName">queue name of the event</param>
    /// <param name="eventData">json data of the event</param>
    /// <returns></returns>
    public abstract Task HandleJson(string eventName, EventT eventData);
}
