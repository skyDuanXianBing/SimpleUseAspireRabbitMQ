using System.Text.Json;

namespace SimpleRabbitmq.Handler;

public abstract class IEventJsonHandler<EventT> : IEventHandler
{
    public Task Handle(string eventName, object eventData)
    {
        ArgumentNullException.ThrowIfNull(eventData);
        var Data = JsonSerializer.Deserialize<EventT>(eventData.ToString());
        return HandleJson(eventName, Data);
    }

    public abstract Task HandleJson(string eventName, EventT eventData);
}
