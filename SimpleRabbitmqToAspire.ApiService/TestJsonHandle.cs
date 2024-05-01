using SimpleRabbitmq;
using SimpleRabbitmq.Handler;

namespace SimpleRabbitmqToAspire.ApiService;

[Event("TestJson")]
public class TestJsonHandle : IEventJsonHandler<TestMessage>
{
    public override Task HandleJson(string eventName, TestMessage eventData)
    {
        Console.WriteLine($"Received message: {eventData.Name}, {eventData.Age}");
        return Task.CompletedTask;
    }
}
public class TestMessage
{
    public string Name { get; set; }
    public int Age { get; set; }
}