using System.Text.Json;

namespace SimpleRabbitmq.Handler;

public abstract class IEventStringHandler : IEventHandler
{
    public Task Handle(string eventName, object eventData)
    {
        ArgumentNullException.ThrowIfNull(eventData);
        var Data = JsonSerializer.Deserialize<string>(eventData.ToString());
        return HandleString(eventName, Data);
    }

    public abstract Task HandleString(string eventName, string eventData);
}
