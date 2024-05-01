using SimpleRabbitmq;
using SimpleRabbitmq.Handler;

namespace SimpleRabbitmqToAspire.ApiService;


[Event("Test")]
public class TestHandle : IEventStringHandler
{

    public override Task HandleString(string eventName, string eventData)
    {
        Console.WriteLine("TestHandle: " + eventName + " " + eventData);
        return Task.CompletedTask;
    }
}
