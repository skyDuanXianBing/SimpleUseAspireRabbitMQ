using System.Text.Json;

namespace SimpleRabbitmq.Handler;

/// <summary>
/// Interface for handling events of type string.
/// </summary>
public abstract class IEventStringHandler : IEventHandler
{
    public Task Handle(string eventName, object eventData)
    {
        ArgumentNullException.ThrowIfNull(eventData);
        var Data = JsonSerializer.Deserialize<string>(eventData.ToString());
        return HandleString(eventName, Data);
    }

    /// <summary>
    /// Handle the event of type string.
    /// </summary>
    /// <param name="eventName">queue name</param>
    /// <param name="eventData">string data</param>
    /// <returns></returns>
    public abstract Task HandleString(string eventName, string eventData);
}
