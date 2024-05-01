using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleRabbitmq;

namespace Test1.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ValuesController(IEventBus eventBus) : ControllerBase
{
    [HttpPost]
    public async Task Test()
    {
        await eventBus.Publish("Test", "Test message");
    }
    [HttpPost]
    public async Task TestJson()
    {
        await eventBus.Publish("TestJson", new TestMessage { Name = "Test", Age = 25 });
    }

}
public class TestMessage
{
    public string Name { get; set; }
    public int Age { get; set; }
}
